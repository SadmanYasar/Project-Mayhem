using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private Collider enemyCollider;
    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private Transform barrel;

    //Patrolling
    [SerializeField] private Vector3 walkPoint;
    [SerializeField]bool walkPointSet;

    //Attacking
    [SerializeField]private float attackRange;
    [SerializeField] private float timeBetweenAttacks = 1.0f;

    [SerializeField] private GameObject Weapon;

    [SerializeField] private GameObject bulletPrefab;
    bool alreadyAttacked;

    [SerializeField]private ParticleSystem muzzleFlash;


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

    //Animation
    [SerializeField] Animator enemyAnim;
    int isWalkingHash;
    int isRunningHash;

    private void Awake() {
        /* isWalkingHash = Animator.StringToHash("Walk");
        isWalkingHash = Animator.StringToHash("Run"); */
        isDead = false;
        agent.updatePosition = false;
        gameObject.name = "Enemy";

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
            if (enemyAnim.GetBool("Walk") == false || enemyAnim.GetBool("Run") == true )
            {
                enemyAnim.SetBool("Run", false);
                enemyAnim.SetBool("Walk", true);
            } 
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
        if ( enemyAnim.GetBool("Run") == false)
        {
            enemyAnim.SetBool("Walk", true);
            enemyAnim.SetBool("Run", true);

        }
        
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
        if (enemyAnim.GetBool("Run") == true)
        {
            enemyAnim.SetBool("Run", false);
            enemyAnim.SetBool("Walk", false);
        }
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            
            //Attack code here
            GameManager.shotByPlayer = 0;
            muzzleFlash.Play();
            PoolManager.instance.ReuseObject(bulletPrefab, barrel.position, barrel.rotation);
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
        enemyCollider.enabled = false;
        enemyAnimator.enabled = false;

        Weapon.transform.SetParent(null);
        Weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Weapon.GetComponent<PIckUpDropController>().enabled = true;
        Weapon.GetComponent<Shoot>().enabled = true;
        GameManager.checkForWin();

    }

} 
