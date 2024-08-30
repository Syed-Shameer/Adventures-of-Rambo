using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 movevector = Vector2.zero;
    private Rigidbody2D rb = null;
    private float moveSpeed = 10f;
    private Animator anim;
    // private float jumpTimeCounter = 0.1f;
    // private float jumpForce = 5f;
    // private float jumpTime = null;
    // private bool isJumping = null;
    private bool isJumping = false;
    private float jumpTimeCounter;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpTime = 0.35f;
    private bool facingRight = true; // Track character's facing direction

   

    private void Awake() {
        input = new CustomInput();
        rb =GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        
    }
    private void OnEnable() {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
        input.Player.jump.performed += OnJumpPerformed; // Subscribe to Jump action
        input.Player.jump.canceled += OnJumpCanceled; // Subscribe to Jump cancel action
    }
    private void OnDisable() {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
        input.Player.jump.performed -= OnJumpPerformed; // Unsubscribe to Jump action
        input.Player.jump.canceled -= OnJumpCanceled; // Unsubscribe to Jump cancel action

    
    }
    private void FixedUpdate() {
        rb.velocity = movevector * moveSpeed;
         RunCheck();
         JumpCheck();
         FlipCharacter();
    }
   
    private void OnMovementPerformed(InputAction.CallbackContext value){
        movevector = value.ReadValue<Vector2>();

    }
    private void OnMovementCanceled(InputAction.CallbackContext value){
        movevector = Vector2.zero;

    }
     private void RunCheck() {
      if (movevector == Vector2.zero)
      {
          anim.SetBool("isRunning",false);
        
      }
      else
      {
          anim.SetBool("isRunning", true );
      }    
      }
    
    // private void JumpCheck(){
    //     if (movevector == Vector2.zero && input.GetKeyDown(KeyCode.Spacebar))
    //     {
    //         anim.SetTrigger("takeoff");
    //         isJumping = true;
    //         jumpTimeCounter = jumpTime;
    //         rb.velocity = Vector2.up * jumpForce;

    //     }
    //     else if(movevector == Vector2.zero){
    //         anim.SetBool("isJumping",false);
    //     } 
    //     else
    //     {
    //         anim.SetBool("isJumping",true);
    //     }
    // }
    private void JumpCheck() {
    if (isJumping && jumpTimeCounter > 0) {
        rb.velocity = Vector2.up * jumpForce;
        jumpTimeCounter -= Time.fixedDeltaTime;
    }

    if (movevector == Vector2.zero && !isJumping) {
        anim.SetBool("isJumping", false);
    } else {
        anim.SetBool("isJumping", true);
    }
}
    // Called when Jump action is performed (button pressed)
    private void OnJumpPerformed(InputAction.CallbackContext context) {
        if (movevector == Vector2.zero && !isJumping) {
            anim.SetTrigger("takeoff");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true); // Set Animator parameter for jumping
        }
}
    // Called when Jump action is canceled (button released)
    private void OnJumpCanceled(InputAction.CallbackContext context) {
        isJumping = false;
        anim.SetBool("isJumping", false); // Reset Animator parameter for jumping
    }
    // Flip the character's direction based on movement
    private void FlipCharacter() {
        if (movevector.x > 0 && !facingRight) {
            Flip();
        } else if (movevector.x < 0 && facingRight) {
            Flip();
        }
    }

    // Flip the character by inverting the local scale
    private void Flip() {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invert the x-axis scale to flip the sprite
        transform.localScale = scale;
    }
}
