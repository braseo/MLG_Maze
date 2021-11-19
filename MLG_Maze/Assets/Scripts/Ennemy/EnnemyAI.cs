using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public LayerMask groundMask, playerMask;

    public Vector3 walkpoint;
    bool walkpointSet;
    public float walkpointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    

    private void Start()
    {
        //Recognize player and navMesh
        player = GameObject.Find("Player(Clone)").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check if player is in range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        //Ennemy attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        //If no player around = patroling
        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        //If player in sight range = chase
        if (playerInSightRange && !playerInAttackRange)
            Chasing();
        //If player in attack range = attack
        if (playerInSightRange && playerInAttackRange)
            Attacking();
    }

    private void Patroling()
    {
        //If no destination = search destination
        if (!walkpointSet) 
            SearchWalkPoint();

        //If destination found = go to
        if (walkpointSet)
            navMeshAgent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkpointSet = false;
    }

    //find a new point to go
    private void SearchWalkPoint()
    {
        float rdmZ = Random.Range(-walkpointRange, walkpointRange);
        float rdmX = Random.Range(-walkpointRange, walkpointRange);

        walkpoint = new Vector3(transform.position.x + rdmX, transform.position.y, transform.position.z + rdmZ);

        //Walkpoint is valid
        if (Physics.Raycast(walkpoint, -transform.up, 2f, groundMask))
            walkpointSet = true;
    }
    //Player is found, go to player
    private void Chasing()
    {
        navMeshAgent.SetDestination(player.position);
    }

    private void Attacking()
    {
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }       
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Gizmos range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

   
}
