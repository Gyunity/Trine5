using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class CannonRope_GH : MonoBehaviour
{
    RectTransform ropeRT;

    bool ropeScale;

    bool ropeRota;
    // Start is called before the first frame update
    void Start()
    {
        ropeRT = GetComponent<RectTransform>();
        ropeScale = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ropeRT.localScale.x > 1.2f)
        {
            ropeScale = false;
        }
        else if (ropeRT.localScale.x < 1)
        {
            ropeScale = true;

        }





        if (ropeScale)
        {
            ropeRT.localScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
        }
        else
        {
            ropeRT.localScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;

        }

    }
}
