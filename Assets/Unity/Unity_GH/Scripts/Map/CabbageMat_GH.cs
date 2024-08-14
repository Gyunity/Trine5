using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageMat_GH : MonoBehaviour
{
    BoxCollider boxcoll;
    void Start()
    {
        boxcoll = GetComponent<BoxCollider>();
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        boxcoll.isTrigger = true;
        Destroy(gameObject, 2);
    }
}
