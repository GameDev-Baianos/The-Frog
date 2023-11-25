using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField ] private float moveSpeed;
    private float ySpeed = 0.75f;
    private float xSpeed = 1.0f;
     private Vector2 moveDelta;

     private bool IsRolling = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        MoveCharacter(); 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsRolling)
            animator.SetTrigger("IsRolling");
    }

     void MoveCharacter()
    {
        // Movement Input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput  = Input.GetAxisRaw("Vertical");



        moveDelta = new Vector2(xInput * xSpeed, yInput * ySpeed);
        moveDelta.Normalize();

        // move the RigidBody2D instead of moving the Transform
        rb.velocity = moveDelta * moveSpeed;

        // animation direction
        animator.SetFloat("XInput", moveDelta.x);
        animator.SetFloat("YInput", moveDelta.y);
        animator.SetFloat("Magnitude", moveDelta.magnitude);
        
    }
}
