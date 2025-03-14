﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageCrush_GH : MonoBehaviour
{
    public GameObject Cabbagepiece;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow") || collision.gameObject.layer == LayerMask.NameToLayer("PlayerSword"))
        {
            SoundManager.instance.PlayStoryEftSound(SoundManager.EStoryEftType.STORY_CABAGGE_BOOM);
            Instantiate(Cabbagepiece, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
