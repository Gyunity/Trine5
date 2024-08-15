using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabbageText_GH : MonoBehaviour
{
    public Image cabbageText;

    bool fadestart = false;

    bool fadeShift = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadestart)
        {
            StartCoroutine(cabbage());
            if(fadeShift)
            {
                cabbageText.color += new Color(0, 0, 0, 0.5f) * Time.deltaTime;
            }
            else
            {
                cabbageText.color -= new Color(0, 0, 0, 0.5f) * Time.deltaTime;

            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            fadestart = true;
    }

    IEnumerator cabbage()
    {
        yield return new WaitForSeconds(5);
        fadeShift = false;
    }
}
