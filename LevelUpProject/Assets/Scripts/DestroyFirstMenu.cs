using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFirstMenu : MonoBehaviour
{
    public void DestroyMenu()
    {
        MenuController mC = FindObjectOfType<MenuController>();
        mC.isReadyForTwo = true;
        Destroy(gameObject);
    }
}
