using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float jumpHeight = 6.5f;
    [SerializeField] float sizeScale = 1.3f;

    [Header("Jump Down")]
    [SerializeField] private float jumpDownSpeed;
    [SerializeField] private float fallMultiplier;
    Vector2 vecGravity; 

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Extra jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpSound;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Vector2 initialScale;

    private float horizontalInput;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        playerSizeScale(); 

        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
  
        Run();
        FlipSprite();     

        if(isGrounded()) {
            coyoteCounter = coyoteTime; // Reset coyote counter when touching ground
            jumpCounter = extraJumps;
        } else {
            coyoteCounter -= Time.deltaTime; // Start decreasing coyote counter when not on the ground
        }

    }

    private void FixedUpdate() {

        // jumpDown acceralation
        if (!isGrounded() && Input.GetAxisRaw("Vertical") < 0) {
            myRigidbody.AddForce(new Vector2(0f, -jumpDownSpeed), ForceMode2D.Impulse);
        }

        if (myRigidbody.velocity.y < 0) {
            myRigidbody.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        // Allow horizontal movement while jumping down
        if (!isGrounded() && myRigidbody.velocity.y < 0)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }
    }


    void playerSizeScale()
    {
        Transform characterTransform = transform;
        characterTransform.localScale *= sizeScale;
        initialScale = characterTransform.localScale;
    }

    void OnMove(InputValue value)
    {
        // store move direction
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isGrounded() && coyoteCounter < 0 && jumpCounter <= 0)
        {
            return;
        }
        if(value.isPressed)
        {
            if (coyoteCounter > 0) {
                SoundManager.instance.PlaySound(jumpSound);
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpHeight);
            } else {
                if (jumpCounter > 0) {
                    SoundManager.instance.PlaySound(jumpSound);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpHeight);
                    jumpCounter--;
                }
            }
            
        }
    }

    bool isGrounded()
    {
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * initialScale.x, initialScale.y);
        }
    }
}
