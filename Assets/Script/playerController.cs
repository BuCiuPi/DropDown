using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
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

    Vector2 moverment;
    float timePressDown = 0;
    float percentOfJumpForce = 0;
    public bool hadjump = true;
    //bool onGround = false;
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
            gameObject.transform.position = new Vector3(ressetPoint.x,ressetPoint.y + 0.5f,ressetPoint.z);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ressetPoint = gameObject.transform.position;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            moverment.x = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            moverment.x = 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moverment.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moverment.x = 0;
        }
        /*moverment.y = Input.GetAxis("Vertical");*/

        // animation trigger
        if (moverment.x > 0)
        {
            obj.transform.localScale = new Vector3(1, 1, 1);
        }
        if (moverment.x < 0)
        {
            obj.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (true)
        {
            Input.GetKey(KeyCode.Space);
        }


        // moving
        if (gameObject.GetComponentInChildren<foot>().onGround() && (!Input.GetKey(KeyCode.Space) || !jumpclick) && hadjump)
        {
            rb.velocity = new Vector2(moverment.x * moveSpeed, rb.velocity.y);
        }

        if (gameObject.GetComponentInChildren<foot>().onGround() && (Input.GetKey(KeyCode.Space) || jumpclick))
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
        if ((Input.GetKey(KeyCode.Space) || jumpHover) && Time.time - timePressDown > 1 && !hadjump && GetComponentInChildren<foot>().onGround())
        {
            jumpCharge(holdTime);
            
        }

        //jump when keyUP
        if (Input.GetKeyUp(KeyCode.Space))
        {
            onBtnJumpUp();
        }
        if (!jumpclick && gameObject.GetComponentInChildren<foot>().onGround() && !hadjump)
        {
            percentOfJumpForce = (Time.time - timePressDown)/*/holdTime*/;
            jumpCharge(percentOfJumpForce);

        }

        //limited the droping speed
        //if (rb.velocity.y < -dropingspeed)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, -dropingspeed);
        //}
        //Debug.Log(rb.velocity);

    }

    private void jumpCharge(float percentOfJumpForce)
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
        
        Debug.Log("right");
    }
    public void onBtnMoveLeftDown()
    {
        moverment.x = -1;

        Debug.Log("left");
    }
    public void OnMoveBtnUp()
    {
        moverment.x = 0;
    }


    bool jumpclick;
    bool jumpHover;
    public void onBtnJumpDown()
    {
        jumpclick = true;
        timePressDown = Time.time;
        GetComponentInParent<playerController>().anim.SetInteger("status", 1);
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

    Vector2 vel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // bouncing Part
        if (!GetComponentInChildren<foot>().onGround())
        {
            rb.velocity = Vector2.Reflect(vel * 0.01f * PercentOfBounce, collision.contacts[0].normal);
        }
        hadjump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //catch the velocity before bouncing
            vel = rb.velocity;
    }



}
