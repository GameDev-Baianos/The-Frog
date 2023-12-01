using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField ] private float moveSpeed;
    private float ySpeed = 0.75f;
    private float xSpeed = 1.0f;
     private Vector2 moveDelta;
     private bool IsAlive = true;

     [Header("Dash Settings")]
     [SerializeField] float dashSpeed = 20f;
     [SerializeField] float dashDuration = 0.25f;
     [SerializeField] float dashCooldown = 1f;
     bool isDashing = false;
     bool canDash = true;

     float auxX = 0, auxY = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        if(isDashing)
            return;
            
        if(IsAlive)
        {
            MoveCharacter();
            Death();
        }
    }

    void Update()
    {
        if(isDashing)
            return;
        
        if(Input.GetKeyDown(KeyCode.Space) && canDash)
            StartCoroutine(Dash());
    }

    void MoveCharacter()
    {
        // Movement Input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput  = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector2(xInput * xSpeed, yInput * ySpeed);
        moveDelta.Normalize();

        if(moveDelta.x != 0 || moveDelta.y != 0)
        {
            auxX = moveDelta.x;
            auxY = moveDelta.y;
        }
 
        rb.velocity = moveDelta * moveSpeed;

        // animation direction
        if(xInput != 0 || yInput != 0)
        {
            animator.SetFloat("XInput", moveDelta.x);
            animator.SetFloat("YInput", moveDelta.y);
        }

        animator.SetFloat("Magnitude", moveDelta.magnitude);
    }

    private void Death()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsAlive", false);
            IsAlive = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(auxX * dashSpeed, auxY * dashSpeed);
        animator.SetTrigger("IsRolling");
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;  
    }
}
