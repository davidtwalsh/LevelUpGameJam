using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Animator startTitleAnimator;
    public GameObject menuFadeObj;
    bool hasStartedTwo = false;
    public bool isReadyForTwo = false;
    public GameObject secondObj;
    float twoTimer = 0;
    public Animator secondAnimator;
    bool hasBegunFadeOutTwo = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyForTwo == true && hasStartedTwo == false)
        {
            secondObj.SetActive(true);
            hasStartedTwo = true;
        }
        if (hasStartedTwo == true)
        {
            twoTimer += Time.deltaTime;
        }
        if (twoTimer >= 3f && hasBegunFadeOutTwo == false)
        {
            hasBegunFadeOutTwo = true;
            secondAnimator.SetBool("beginFadeOut", true);
        }
    }

    public void BeginFirstFadeOut()
    {
        menuFadeObj.SetActive(true);
        startTitleAnimator.SetBool("isFade", true);
    }
}
