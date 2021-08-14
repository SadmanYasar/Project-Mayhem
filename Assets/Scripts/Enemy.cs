using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    [SerializeField]bool walkPointSet;

    //Attacking
    [SerializeField]private float attackRange;
    public float timeBetweenAttacks;
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

    [SerializeField]private int speed;

    Vector3 velocity = Vector3.zero;

    private void Awake() {
        agent.updatePosition = false;
        StartCoroutine(FOVRoutine());
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
            agent.speed = speed*Time.fixedDeltaTime;
        }
        if ( !walkPointSet ) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            //transform.position = Vector3.Lerp(transform.position, agent.nextPosition, 1f);
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
        /* if ( Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround) )
        {
            walkPointSet = true;
            
            
        } */
    
    }

    public void ChasePlayer() {
        if ( agent.speed == 0 )
        {
            agent.speed = speed*Time.fixedDeltaTime;
        }
        agent.SetDestination(player.position);
        //transform.position = Vector3.Lerp(transform.position, agent.nextPosition, 1f);
        transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f );
        
        float playerEnemyDist = Vector3.Magnitude(player.position - transform.position);
        if ( playerEnemyDist <= attackRange )
        {
            AttackPlayer();
        }
    }

    public void AttackPlayer() {
        //make sure enemy doesnt move
        //agent.SetDestination(transform.position);
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

    



}
