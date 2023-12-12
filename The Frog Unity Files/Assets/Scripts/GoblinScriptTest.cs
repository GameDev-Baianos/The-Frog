using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScriptTest : MonoBehaviour
{

    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 moveDelta;
    private float xSpeed = 1.0f;
    [SerializeField ] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        // Movement Input
        float xInput = Input.GetAxisRaw("Horizontal");

        moveDelta = new Vector2(xInput * xSpeed, 0);
        moveDelta.Normalize();
 
        rb.velocity = moveDelta * moveSpeed;

        // animation direction
        if(xInput != 0)
        {
            animator.SetFloat("XWalking", moveDelta.x);
        }

        animator.SetFloat("Magnitude", moveDelta.magnitude);
    }
}
