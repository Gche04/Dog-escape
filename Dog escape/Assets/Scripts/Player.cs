using System.Collections;
using UnityEngine;

public class Player : Creature
{
    Rigidbody playerRb;

    [SerializeField] float speed = 14f;
    [SerializeField] float turn = 100f;
    float jump = 20f;

    [SerializeField] GameObject cameraController;
    [SerializeField] float cameraRotationSpeed = 35f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        //get child of gameobject then get animator component
        animator = transform.GetChild(0).GetComponent<Animator>();

        health = GameManager.Instance.GetPlayerHealth();
        runSpeed = speed;
        jumpSpeed = jump;
        turnSpeed = turn;

        if (!GameManager.Instance.GetIsANewGame())
        {
            transform.position = GameManager.Instance.GetSavedPlayerPos();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Move();
        }
        else
        {
            GameManager.Instance.SetIsPlayerAlive(isAlive);
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
        }
    }

    void LateUpdate()
    {
        CameraMovement();
    }

    protected override void Move()
    {
        //gets user input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // calculates turn
        float turn = horizontalInput * turnSpeed * Time.deltaTime;
        // sets player rotation
        Quaternion turnRot = Quaternion.Euler(0f, turn, 0f);
        // rotates player
        playerRb.MoveRotation(playerRb.rotation * turnRot);
        
        // calculates player move direction
        // normalized* gives movement magnitude of 1, preventing faster diagonal movement
        Vector3 moveDirection = verticalInput * transform.forward.normalized;
        
        //set speed based on movement
        
        float updateSpeed = (moveDirection.magnitude > 0 || turnRot.y != 0) ? speed : 0f;
        // moves player
        playerRb.linearVelocity = updateSpeed * moveDirection;
        

        animator.SetFloat("Speed_f", updateSpeed);
    }

    void CameraMovement()
    {
        cameraController.transform.SetPositionAndRotation(transform.position, Quaternion.Slerp(
            cameraController.transform.rotation, 
            transform.rotation, 
            cameraRotationSpeed * Time.deltaTime
            )
        );
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dog1") ||
            collision.gameObject.CompareTag("Dog2") ||
            collision.gameObject.CompareTag("Dog3") ||
            collision.gameObject.CompareTag("Dog4")
        )
        {
            TakeDamage();
            GameManager.Instance.SetPlayerHealth(health);
            speed += jumpSpeed;
            StartCoroutine(BackToNormalSpeed());

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Live"))
        {
            AddHealth();
            GameManager.Instance.SetPlayerHealth(health);
        }

        else if (other.gameObject.CompareTag("Food"))
        {
            GameManager.Instance.ReduceFood();
        }
        Destroy(other.gameObject);
    }

    IEnumerator BackToNormalSpeed()
    {
        yield return new WaitForSeconds(1);
        speed -= jumpSpeed;
    }

}
