using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    private bool onTheGround = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        onTheGround = true;
        if (GetComponentInParent<PlayerController>().anim.GetInteger("status") == 4)
        {
            GetComponentInParent<PlayerController>().anim.SetInteger("status", 5);
        }
        else
        {
            GetComponentInParent<PlayerController>().anim.SetInteger("status", 0);
        }

        GetComponentInParent<PlayerController>().hadjump = true;

        GetComponentInParent<PlayerController>().setVel0();

        Debug.Log("on ground");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTheGround = false;
        GetComponentInParent<PlayerController>().anim.SetInteger("status", 2);
    }


    public bool OnGround()
    {
        return onTheGround;
    }

    
}
