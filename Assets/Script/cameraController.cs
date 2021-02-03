using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    GameObject obj;

    public GameObject player;
    float teleportValue;
    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        teleportValue = Camera.main.orthographicSize;
        Debug.Log(teleportValue);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y - obj.transform.position.y >= teleportValue)
        {
            Debug.Log("camera func +");
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + teleportValue*2, obj.transform.position.z);
        }
        if (player.transform.position.y - obj.transform.position.y <= -teleportValue)
        {
            Debug.Log("camera func -");
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + -teleportValue*2, obj.transform.position.z);
        }
    }
}
