using UnityEngine;

public class Dog : Creature
{
    [SerializeField] float speed = 10;
    [SerializeField] float turn = 100;

    Rigidbody dogRb;

    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        dogRb = GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (GameManager.Instance.GetIsPlayerAlive())
        {
            // calculation to get players position
            // normalized* gives movement magnitude of 1, preventing faster diagonal movement
            Vector3 moveDirection = (player.transform.position - transform.position).normalized;
            // move dog to players position
            dogRb.linearVelocity = speed * moveDirection;


            // only rotate when their is movement
            if (moveDirection != Vector3.zero)
            {
                //calculates rotation needed to point dog forward direction along moveDirection
                Quaternion desiredRot = Quaternion.LookRotation(moveDirection);
                //interpolates between current and desired rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, turn * Time.deltaTime);
            }
        }
        else
        {
            speed = 0;
            animator.SetFloat("Speed_f", speed);
            animator.SetBool("Eat_b", true);
        }

    }

}
