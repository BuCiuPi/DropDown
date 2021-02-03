using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foot : MonoBehaviour
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
        GetComponentInParent<playerController>().anim.SetInteger("status", 0);
        GetComponentInParent<playerController>().hadjump = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTheGround = false;
        GetComponentInParent<playerController>().anim.SetInteger("status", 2);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }



    public bool onGround()
    {
        return onTheGround;
    }
}
