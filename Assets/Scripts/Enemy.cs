using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private Collider enemyCollider;
    [SerializeField] private Animator enemyAnimator;

    //Patrolling
    [SerializeField] private Vector3 walkPoint;
    [SerializeField]bool walkPointSet;

    //Attacking
    [SerializeField]private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;


    //pathfinding
    [SerializeField]private GameObject[] walkPoints;
    [SerializeField]private int wpIndex;

    //FOV
    public float radius;
    [Range(0,360)]
    public float angle;

    public LayerMask obstructionMask;

    public bool canSeePlayer;

    IEnumerator fovCheck;

    //AI motion
    [SerializeField]private int speed;

    Vector3 velocity = Vector3.zero;

    public bool isDead;

    private void Awake() {
        isDead = false;
        agent.updatePosition = false;
        fovCheck = FOVRoutine();
        StartCoroutine(fovCheck);
    }


    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, whatIsPlayer);
        //checks if player in fov
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //check if in angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //check if wall between enemy and player
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    //no obstacles
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    

    public void Patrolling() {
        if ( agent.speed == 0 )
        {
            agent.speed = speed;
        }
        if ( !walkPointSet ) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f );
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude <= 0.1f)
        {
            //walkpoint reached
            walkPointSet = false;
           
        }
    
    
    
    }
    public void SearchWalkPoint() {
        
        if ( wpIndex >= walkPoints.Length)
        {
            wpIndex = 0;
        }
        
        //move in squares
        walkPoint = walkPoints[wpIndex].transform.position;
        
        wpIndex++;
        

        walkPointSet = true;
    
    }

    public void ChasePlayer() {
        if ( agent.speed == 0 )
        {
            agent.speed = speed;
        }
        agent.SetDestination(player.position);
        transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f );
        
        float playerEnemyDist = Vector3.Magnitude(player.position - transform.position);
        if ( playerEnemyDist <= attackRange )
        {
            AttackPlayer();
        }
    }

    public void AttackPlayer() {
        //make sure enemy doesnt move
        agent.speed = 0;
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here

            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    public void Die() {
        isDead = true;
        StopCoroutine(fovCheck);
        enemyCollider.isTrigger = false;
        enemyAnimator.enabled = false;
    }



}
