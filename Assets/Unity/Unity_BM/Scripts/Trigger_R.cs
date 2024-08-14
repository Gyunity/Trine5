using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_R : MonoBehaviour
{
    
    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Player_equipment")
        {
            //전기충격
            Debug.Log("R버튼 클릭");

            isOn = true;
        }
    }
}
