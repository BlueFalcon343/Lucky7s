using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange;
    public bool playerInSightRange;
    bool playerInvisible;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        playerInvisible = FindObjectOfType<PlayerController>().isInvisible;
    }

    private void Update()
    {
        //Checks for player in sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        //Checks to see if the player is invisible
        playerInvisible = FindObjectOfType<PlayerController>().isInvisible;

        if (!playerInSightRange) Patroling();
        if (playerInSightRange && !playerInvisible) ChasePlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!alreadyAttacked)
        {
            //Attack code here
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ChangeSpook(2);
            }
            //End of Attack code

            // Starts break between attacks
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // Ends break between attacks
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // Display sight range for testing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
