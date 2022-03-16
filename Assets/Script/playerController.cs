using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject obj;

    public float jumpForce = 15;
    public float moveSpeed = 5;
    public float dropingspeed = 15;
    public float jumplenght = 10;
    public float PercentOfBounce = 100;
    public float holdTime = 1;

    public Rigidbody2D rb;
    public Animator anim;

    public Vector2 moverment;
    float timePressDown = 0;
    float percentOfJumpForce = 0;
    public bool hadjump = true;
    bool isSlippy = false;
    //bool OnGround = false;
    Collision2D coli;

    Vector3 ressetPoint;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        ressetPoint = gameObject.transform.position;
    }



    // Update is called once per frame
    void Update()
    {

        // resetchar
        if (Input.GetKeyDown(KeyCode.R))
        {
            obj.transform.position = new Vector3(ressetPoint.x, ressetPoint.y + 0.5f, ressetPoint.z);
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ressetPoint = obj.transform.position;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !isSlippy)
        {
            //GetComponentInParent<PlayerController>().anim.SetInteger("status", 0);
            onBtnMoveLeftDown();
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !isSlippy)
        {
            //GetComponentInParent<PlayerController>().anim.SetInteger("status", 0);
            onBtnMoveRightDown();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            OnMoveBtnUp();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            OnMoveBtnUp();
        }


        ///*moverment.y = Input.GetAxis("Vertical");*/


        // moving
        if (obj.GetComponentInChildren<Foot>().OnGround() && (!Input.GetKey(KeyCode.Space) || !jumpclick) && hadjump && moverment.x != 0 && !isSlippy)
        {
            rb.velocity = new Vector2(moverment.x * moveSpeed, rb.velocity.y);
        }

        if (obj.GetComponentInChildren<Foot>().OnGround() && (Input.GetKey(KeyCode.Space) || jumpclick))
        {
            rb.velocity = new Vector2(0, 0);
        }

        ///<summary>
        ///the part of jumping
        /// </summary>

        //cactch time when press key down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onBtnJumpDown();
        }

        // jumping when time out
        if ((Input.GetKey(KeyCode.Space) || jumpHover) && Time.time - timePressDown > 1 && !hadjump && GetComponentInChildren<Foot>().OnGround())
        {
            JumpCharge(holdTime);

        }

        //jump when keyUP
        if (Input.GetKeyUp(KeyCode.Space))
        {
            onBtnJumpUp();
        }
        if (!jumpclick && obj.GetComponentInChildren<Foot>().OnGround() && !hadjump)
        {
            percentOfJumpForce = (Time.time - timePressDown)/*/holdTime*/;
            JumpCharge(percentOfJumpForce);

        }
        if (!GetComponentInChildren<Foot>().OnGround() || true)
        {
            GetComponent<PlayerController>().anim.SetFloat("Velocity.X", rb.velocity.x);
            GetComponent<PlayerController>().anim.SetFloat("Velocity.Y", rb.velocity.y);
        }

        //limited the droping speed
        if (rb.velocity.y < -dropingspeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -dropingspeed);
        }
        //Debug.Log(rb.velocity);

    }

    private void JumpCharge(float percentOfJumpForce)
    {
        if ((Input.GetKey(KeyCode.RightArrow) || moverment.x == 1) || (Input.GetKey(KeyCode.LeftArrow) || moverment.x == -1))
        {
            if (Input.GetKey(KeyCode.RightArrow) || moverment.x == 1)
            {
                rb.velocity = new Vector2(jumplenght, jumpForce * percentOfJumpForce);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || moverment.x == -1)
            {
                rb.velocity = new Vector2(-jumplenght, jumpForce * percentOfJumpForce);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, jumpForce * percentOfJumpForce);
        }
    }
    
    public void onBtnMoveRightDown()
    {

            moverment.x = 1;

            //animation for Right
            GetComponent<PlayerController>().anim.SetFloat("Moverment", 2);
            //Debug.Log("right");

    }
    public void onBtnMoveLeftDown()
    {
            moverment.x = -1;

            //animation for Left
            GetComponent<PlayerController>().anim.SetFloat("Moverment", -2);
            //Debug.Log("left");
    }
    public void OnMoveBtnUp()
    {
        moverment.x = 0;
        if (GetComponentInChildren<Foot>().OnGround())
        {
            rb.velocity = new Vector2(0, 0);
        }
        // Idle animation
        if (GetComponent<PlayerController>().anim.GetFloat("Moverment") == 2)
        {
            GetComponent<PlayerController>().anim.SetFloat("Moverment", 1);
        }
        else
        {

        }


        if (GetComponent<PlayerController>().anim.GetFloat("Moverment") == -2)

        {
            GetComponent<PlayerController>().anim.SetFloat("Moverment", -1);
        }
        else
        {

        }
        Debug.Log(rb.velocity);
    }


    bool jumpclick;
    bool jumpHover;
    public void onBtnJumpDown()
    {
        jumpclick = true;
        timePressDown = Time.time;
        GetComponentInParent<PlayerController>().anim.SetInteger("status", 1);
        hadjump = false;
    }
    public void onBtnJumpUp()
    {
        jumpclick = false;
    }

    public void onBtnJumpEnter()
    {
        jumpHover = true;
    }

    public void onBtnJumpExit()
    {
        jumpHover = false;
    }

    public Vector2 vel;
    
    public void setVel0()
    {
        rb.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // bouncing Part
        if (!GetComponentInChildren<Foot>().OnGround())
        {
            //rb.velocity = new Vector2(0, 0);
            //rb.velocity = Vector2.Reflect(vel * 0.01f * PercentOfBounce, collision.contacts[0].normal);
            GetComponentInParent<PlayerController>().anim.SetInteger("status", 4);
        }
        if (collision.gameObject.tag == "Slippy Platform")
        {
            isSlippy = true;
        }
        hadjump = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slippy Platform")
        {
            isSlippy = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //catch the velocity before bouncing
            vel = rb.velocity;
    }



}
