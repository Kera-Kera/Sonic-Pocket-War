using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private float speed = 0f;
    [SerializeField]
    private float maxSpeed = 5f, acceleration = 1f, jumpHeight = 5f;

    [SerializeField]
    private LayerMask platformLayerMask;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxcollider2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxcollider2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        GroundCheck();
        InputController();
        PlayerSpeed();

    }

    private void Jump()
    {
        rigidbody2d.velocity.Set(rigidbody2d.velocity.x,0);
        rigidbody2d.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
    }

    private void PlayerSpeed()
    {
        if (speed > maxSpeed)
            speed = maxSpeed;
        if (speed < -maxSpeed)
            speed = -maxSpeed;

        rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
    }

    private void InputController()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            speed -= acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
            speed += acceleration * Time.deltaTime;
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && GroundCheck())
        {
            speed /= 1.003f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            Jump();
        }
    }

    private bool GroundCheck()
    {
        float lengthdown = 0.1f;

        RaycastHit2D hit = Physics2D.BoxCast(boxcollider2d.bounds.center, boxcollider2d.bounds.size, 0f, Vector2.down, lengthdown, platformLayerMask);
        return hit.collider;
    }
}
