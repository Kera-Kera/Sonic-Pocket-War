using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 2f;

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


        if (Input.GetKey(KeyCode.LeftArrow))
            rigidbody2d.velocity = new Vector2(speed * -1, rigidbody2d.velocity.y);
        if (Input.GetKey(KeyCode.RightArrow))
            rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rigidbody2d.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);

        isGrounded = false;
    }

    private void GroundCheck()
    {
        float lengthdown = 0.15f;

        RaycastHit2D hit = Physics2D.BoxCast(boxcollider2d.bounds.center, boxcollider2d.bounds.size, 0f, Vector2.down, lengthdown, platformLayerMask);
        isGrounded = hit.collider;
    }
}
