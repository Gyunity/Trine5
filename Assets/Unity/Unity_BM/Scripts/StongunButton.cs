using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StongunButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Dummy")
        {
            Debug.Log("���� ���");
            //���� ���
        }
    }
}
