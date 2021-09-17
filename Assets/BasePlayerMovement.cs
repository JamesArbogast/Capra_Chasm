using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BasePlayerMovement : MonoBehaviour {

    //player rigidbody
    public Rigidbody2D playerRigidBody;
    //regular movement speed
    public float movementSpeed;
    public bool isMoving;
    public bool isSprinting;
    //sprinting
    public float sprintSpeed;
    public float originalSpeed;
    public float sprintTime;
    public float originalSprintTime;
    //jumping
    public float jumpVelocity;
    public bool isGrounded;
    //wall jumping
    public bool isTouchingFront;
    public Transform frontCheck;
    //wall sliding
    public bool wallSliding;
    //wall climbing
    public bool wallClimbing;
    public float wallClimbingSpeed;
    public float wallClimbTime;
    public float originalWallClimbTime;
    //player taking damage
    public PlayerStats playerStats;
    public BaseEnemyDamage enemyDamage;

    // Use this for initialization
    void Start ()
    {
        playerRigidBody = transform.GetComponent<Rigidbody2D>();
        isGrounded = true;
        wallSliding = false;
        //for regaining normal speed after sprint
        originalSpeed = movementSpeed;
        playerRigidBody.gravityScale = 2f;
        //for player damage from enemy collision
        playerStats = transform.GetComponent<PlayerStats>();
        enemyDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemyDamage>();
        originalWallClimbTime = wallClimbTime;
        originalSprintTime = sprintTime;

	}

    // Update is called once per frame
    void Update()
    {

        //weight while grounded
        if (isGrounded)
        {
            playerRigidBody.gravityScale = 2f;
        }

        //moving left and right
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if(!wallClimbing)
            {
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;
                isMoving = true;
                //transform.rotation.y = 180; //This is for rotating sprite images
            }
        }
        else if
        (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!wallClimbing)
            {
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
                isMoving = true;
                //transform.rotation.y = 0; //This is for rotating sprite images
            }
        }
        else
        {
            isMoving = false;
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            playerRigidBody.velocity = Vector2.up * jumpVelocity;
            //transform.Translate(Vector3.up * 100 * Time.deltaTime, Space.World);
            isGrounded = false;
            isMoving = true;
        }

        //sprinting
        if(Input.GetKeyDown(KeyCode.J) && sprintTime > 0)
        {

            isSprinting = true;

            
        } else if (Input.GetKeyUp(KeyCode.J) || sprintTime <= 0)
        {
            movementSpeed = originalSpeed;
            isSprinting = false;
        }

        if (isSprinting == true)
        {
            movementSpeed = sprintSpeed;
            sprintTime -= Time.deltaTime;
        }
        else if (isSprinting == false)
        {
            movementSpeed = originalSpeed;
        }

        if(sprintTime <= 0)
        {
            sprintTime = 0;
        }

        //wall jumping
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            playerRigidBody.velocity = Vector2.up * jumpVelocity;
            //transform.Translate(Vector3.up * 100 * Time.deltaTime, Space.World);
            //wallSliding = false;
        }

        if(wallSliding == true)
        {
            playerRigidBody.gravityScale = .4f;
        }
        else
        {
            playerRigidBody.gravityScale = 2f;
        }

        //wall climbing
        if(wallSliding == true && Input.GetKeyDown(KeyCode.W) && wallClimbTime > 0)
        {
            wallClimbing = true;    
        }

        if (wallClimbing == true)
        {
            playerRigidBody.gravityScale = 0f;
            playerRigidBody.mass = 0f;
            transform.position += Vector3.up * wallClimbingSpeed * Time.deltaTime;
            wallClimbTime -= Time.deltaTime;
        }

        //getting hit by an enemy


}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Player Hurt!");
            playerStats.currentPlayerHealth -= enemyDamage.collideDamage;
        }
    }
}
