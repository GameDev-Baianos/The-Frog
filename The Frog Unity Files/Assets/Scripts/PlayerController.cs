using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public static PlayerController instance;

    [SerializeField ] private float moveSpeed;
    private float ySpeed = 0.75f;
    private float xSpeed = 1.0f;
    private Vector2 moveDelta;
    private bool IsAlive = true;
    float auxX = 0, auxY = -1;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing = false;
    bool canDash = true;

    [Header("HeavyAttack Settings")]
    [SerializeField] float dashHAttack = 15f;
    [SerializeField] float HADuration = 0.25f;
    public bool isHAttacking = false;

    [Header("LightAttack Settings")]
    public bool isAttacking = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        if(isDashing || isAttacking || isHAttacking)
            return;
            
        if(IsAlive)
        {
            MoveCharacter();
            //Death();
        }
    }

    void Update()
    {
        if(isDashing)
            return;
        
        StartCoroutine(Dash());
        StartCoroutine(HeavyAttack());
        Attack();
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

/*
    private void Death()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsAlive", false);
            IsAlive = false;
        }
    }
    */

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            isAttacking = true;
        }
    }
    
    private IEnumerator Dash()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash)
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
    
    private IEnumerator HeavyAttack()
    {
        if(Input.GetKeyDown(KeyCode.K) && !isHAttacking)
        {
            isHAttacking = true;
            yield return new WaitForSeconds(0.3f);
            rb.velocity = new Vector2(auxX * dashHAttack, auxY * dashHAttack);
            yield return new WaitForSeconds(HADuration);
            isHAttacking = false;
        }
    }
}
