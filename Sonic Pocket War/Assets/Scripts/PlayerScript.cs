using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speed = 0f;

    public float maxSpeed = 5f;

    public float minSpeed = -5f;

    public float acceleration = 1f; 

    public LayerMask platformLayerMask;

    public float jumpHeight = 5f;

    [SerializeField]private bool isGrounded = false;

    Rigidbody2D rigidbody2d;

    BoxCollider2D boxcollider2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxcollider2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        GroundCheck();


        //speed -= acceleration * Time.deltaTime;

        if (speed > maxSpeed)
            speed = maxSpeed;

        if (speed < minSpeed)
            speed = minSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
            speed -= acceleration * Time.deltaTime;
            

        if (Input.GetKey(KeyCode.RightArrow))
            speed += acceleration * Time.deltaTime;

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && GroundCheck())
        {
            speed /= 1.003f;
        }

            rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            Jump();
        }
    }

    private void Jump()
    {
        rigidbody2d.velocity.Set(rigidbody2d.velocity.x,0);
        rigidbody2d.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);

        isGrounded = false;
    }

    private bool GroundCheck()
    {
        float lengthdown = 0.1f;

        RaycastHit2D hit = Physics2D.BoxCast(boxcollider2d.bounds.center, boxcollider2d.bounds.size, 0f, Vector2.down, lengthdown, platformLayerMask);
        return hit.collider;
    }
}
