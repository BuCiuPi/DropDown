using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject obj;

    public GameObject player;
    public GameObject toBeContinueUI;

    float teleportValue;

    public float atFloor;
    // Start is called before the first frame update
    void Start()
    {
        atFloor = 1f;
        obj = gameObject;
        teleportValue = Camera.main.orthographicSize;
        Debug.Log(teleportValue);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y - obj.transform.position.y >= teleportValue)
        {
            Debug.Log("camera func Up");
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + teleportValue*2, obj.transform.position.z);
            atFloor += 1;
        }
        if (player.transform.position.y - obj.transform.position.y <= -teleportValue)
        {
            Debug.Log("camera func Down");
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + -teleportValue*2, obj.transform.position.z);
            atFloor += -1;
        }
        if (player.transform.position.x - obj.transform.position.x >= teleportValue *2)
        {
            Debug.Log("camera func Right");
            obj.transform.position = new Vector3(obj.transform.position.x + teleportValue * 4, obj.transform.position.y, obj.transform.position.z);
            atFloor += 0.1f;
        }
        if (player.transform.position.x - obj.transform.position.x <= -teleportValue *2)
        {
            Debug.Log("camera func Left");
            obj.transform.position = new Vector3(obj.transform.position.x + -teleportValue * 4, obj.transform.position.y, obj.transform.position.z);
            atFloor += -0.1f;
        }

        if (atFloor == 3.1f && toBeContinueUI.activeSelf == false)
        {
            toBeContinueUI.SetActive(true);
        }
    }
}
