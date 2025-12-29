using UnityEngine;
using UnityEngine.AI;

public class IntelligentDog : Creature
{

    [SerializeField] float speed = 10;
    [SerializeField] float turn = 100;

    Rigidbody dogRb;
    NavMeshAgent dogNMA;

    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        runSpeed = speed;
        turnSpeed = turn;
        player = GameObject.Find("Player");
        dogRb = GetComponent<Rigidbody>();
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
        if (GameManager.Instance.GetIsPlayerAlive())
        {
            Debug.Log(GameManager.Instance.GetIsPlayerAlive());
            dogNMA.SetDestination(player.transform.position);
        }
    }

}
