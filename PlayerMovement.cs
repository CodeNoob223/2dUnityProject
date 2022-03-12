using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        //Grab references
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput* speed,body.velocity.y);
        //Flip player using X (Horizontal) Axis
        if(horizontalInput > 0f)
            transform.localScale = new Vector2(0.5f,0.5f);
        else if (horizontalInput < 0f)
            transform.localScale = new Vector2(-0.5f,0.5f);
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        //Set animator parameters
        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, 6);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
