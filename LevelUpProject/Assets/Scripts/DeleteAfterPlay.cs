﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterPlay : MonoBehaviour
{
    public float deleteTime;

    // Update is called once per frame
    void Update()
    {
        deleteTime -= Time.deltaTime;

        if (deleteTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
