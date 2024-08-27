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
    

    private void Awake() {
        input = new CustomInput();
        rb =GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        
    }
    private void OnEnable() {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }
    private void OnDisable() {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    
    }
     private void FixedUpdate() {
         rb.velocity = movevector * moveSpeed;
         Debug.Log("Velocity Set To: " + rb.velocity);
         Debug.Log("Position: " + transform.position);
         RunCheck();
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
    
}
