using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //config
    public float runSpeed = 6f;
    public float jumpSpeed = 5f;
    public float climbSpeed = 6f;
    public float wallJumps = 2f;
    public Vector2 deathFlySpeed = new Vector2(20f, 20f);
    //cached component references
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    Animator myAnimator;
    float startingGravityScale;
    
    //state
    bool isAlive = true;
    bool canWallJump = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        startingGravityScale = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        Jump();
        WallJump();
        ClimbLadder();
        PlayerDies();
        FlipSprite();

        if (wallJumps > 0)
        {
            canWallJump = true;
        }
        else
        {
            canWallJump = false;
        }

    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        //animation
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }

    private void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void WallJump()
    {

        if(!myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) || canWallJump == false) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump") && canWallJump == true)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            wallJumps--;
        }

    }

    private void ClimbLadder()
    {
        if (!myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = startingGravityScale;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);       
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        //animation
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;


        if (playerHasHorizontalSpeed)
        //if the player is moving horizontally
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void PlayerDies()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            GetComponent<Rigidbody2D>().velocity = deathFlySpeed;  
            isAlive = false;
            myAnimator.SetTrigger("Dying");
        }
    }
}
