using UnityEngine;

public class GoblinScriptTest : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public float speed;
   

    private float distance;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        //direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // animation direction
        if(direction.x != 0 || direction.y != 0)
        {
            animator.SetFloat("XInput", direction.x);
        }

        if(distance < 10)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
