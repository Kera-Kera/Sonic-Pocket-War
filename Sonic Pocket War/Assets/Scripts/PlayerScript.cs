using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Dictionary<string,bool> stateArray = new Dictionary<string, bool>();

    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float maxSpeed = 5f, acceleration = 1f, jumpHeight = 5f;

    [SerializeField]
    private LayerMask platformLayerMask;





    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxcollider2d;
    private SpriteRenderer objSpriteRenderer;



    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxcollider2d = GetComponent<BoxCollider2D>();

        //sets adds all the states to the object dictionary

        stateArray.Add("isIdle", false);
        stateArray.Add("isMoving", false);
        stateArray.Add("isJumping", false);
        stateArray.Add("isFalling", false);
        stateArray.Add("isAttacking", false);


        objSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        GroundCheck();
        InputController();
        PlayerSpeed();;

    }


    // Applies a force to the rigidbody to move the object

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

        // Inputs for right and left actions - Leads to directional movement respectively
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.RightArrow))
        {
            setStateFalse();
            stateArray["isMoving"] = true;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                objSpriteRenderer.flipX = true;

                speed -= acceleration * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                objSpriteRenderer.flipX = false;
                speed += acceleration * Time.deltaTime;
            }
        }


        // 

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && GroundCheck())
        {

            if (speed > -0.03 && speed < 0.03)
            {
                speed = 0;
                StartCoroutine(idleTimer());
            }
            else
            { 
                speed /= 1.003f;
            }
        }


        // Input for Jump action, checks whether on ground before running function

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

    //Used to set the state to idle after 5 seconds

    IEnumerator idleTimer()
    {
        yield return new WaitForSeconds(5);
        setStateFalse();
        stateArray["isIdle"] = true;
    }

    // Sets all states of the player to false, to be run before setting any state to clear all other states

    private void setStateFalse()
    {

        stateArray["isIdle"] = false;
        stateArray["isMoving"] = false;
        stateArray["isJumping"] = false;
        stateArray["isFalling"] = false;
        stateArray["isAttacking"] = false;


    }


}
