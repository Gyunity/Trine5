﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger_BM : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

        }
    }
}
