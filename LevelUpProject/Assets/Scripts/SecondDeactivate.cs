using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDeactivate : MonoBehaviour
{
    public GameObject thirdObj;

    public void Deactivate()
    {
        thirdObj.SetActive(true);
        Destroy(gameObject);
    }
}
