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
    bool walkPointSet;

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

    private void Awake() {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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

    private void Update() {
        //check sight and attack range
        /* playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer(); */

        if ( canSeePlayer ) {
            ChasePlayer();
        } else Patrolling();
        
    }

    

    private void Patrolling() {
        if ( !walkPointSet ) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude <= 0.1f)
        {
            //walkpoint reached
            walkPointSet = false;
        }
    
    
    
    }
    private void SearchWalkPoint() {
        //calculate random point in range
        /* float randomZ =  Random.Range(-walkPointRange, walkPointRange);
        float randomX =  Random.Range(-walkPointRange, walkPointRange); */

        //walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if ( wpIndex >= walkPoints.Length)
        {
            wpIndex = 0;
        }
        walkPoint = walkPoints[wpIndex].transform.position;
        wpIndex++;

        if ( Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround) )
        {
            walkPointSet = true;
        }
    
    }

    private void ChasePlayer() {
        agent.SetDestination(player.position);
        float playerEnemyDist = Vector3.Magnitude(player.position - transform.position);
        if ( playerEnemyDist <= attackRange )
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer() {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);
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

    /* private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    } */

    



}
