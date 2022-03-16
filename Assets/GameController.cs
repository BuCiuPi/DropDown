using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject tutorialUI;
    public GameObject toBeContinueUI;

    private void Awake()
    {

        tutorialUI.SetActive(true);
        toBeContinueUI.SetActive(false);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideTutorialUI(bool isHide)
    {
        tutorialUI.SetActive(!isHide);
    }

    public void HidetoBeContinueUI(bool isHide)
    {
        toBeContinueUI.SetActive(!isHide);
    }
}
