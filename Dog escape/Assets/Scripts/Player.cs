using System.Collections;
using UnityEngine;

public class Player : Creature
{
    Rigidbody playerRb;

    [SerializeField] float speed = 14f;
    [SerializeField] float turn = 100f;
    float jump = 20f;

    //float cameraPositionOffset = 24f;
    

    int healtCount = 5;
    //int foodLeft = 20;
    //public float bounary = 49f;

    //Camera mainCamera;
    [SerializeField] GameObject cameraController;
    [SerializeField] float cameraRotationSpeed = 35f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //mainCamera = Camera.main;

        health = healtCount;
        runSpeed = speed;
        jumpSpeed = jump;
        turnSpeed = turn;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isAlive)
        //{
            Move();
        //}
        
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

        // calculates player move direction
        // normalized* gives movement magnitude of 1, preventing faster diagonal movement
        Vector3 moveDirection = speed * verticalInput * transform.forward;
        // moves player
        playerRb.linearVelocity = new Vector3(moveDirection.x, playerRb.linearVelocity.y, moveDirection.z);
        // calculates turn
        float turn = horizontalInput * turnSpeed * Time.deltaTime;
        // sets player rotation
        Quaternion turnRot = Quaternion.Euler(0f, turn, 0f);
        // rotates player
        playerRb.MoveRotation(playerRb.rotation * turnRot);
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
        if (collision.gameObject.CompareTag("Dog"))
        {
            TakeDamage();
            speed += jumpSpeed;
            StartCoroutine(BackToNormalSpeed());

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Live"))
        {
            AddHealth();
        }

        else if (other.gameObject.CompareTag("Food"))
        {
            //foodLeft--;
        }
        Destroy(other.gameObject);
    }

    IEnumerator BackToNormalSpeed()
    {
        yield return new WaitForSeconds(1);
        speed -= jumpSpeed;
    }

}
