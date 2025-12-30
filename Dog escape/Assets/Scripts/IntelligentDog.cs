using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class IntelligentDog : Creature
{
    NavMeshAgent dogNMA;

    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        dogNMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        // Set the agent's destination. The NavMeshAgent handles the pathfinding and avoidance.
        if (GameManager.Instance.GetPlayerHealth() > 0)
        {
            dogNMA.SetDestination(player.transform.position);
        }else
        {
            animator.SetFloat("Speed_f", 0);
            animator.SetBool("Eat_b", true);
        }
    }

}
