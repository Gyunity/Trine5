using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StongunButton : MonoBehaviour
{
    public GameObject stungun;

    public bool isOn = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Dummy")
        {
            //�������
            Debug.Log("���� ���");

            //Stungun �� ������Ʈ���� StungunMove ������Ʈ�� ��������.
            StungunMove stunmove = stungun.GetComponent<StungunMove>();
            //������ ������Ʈ���� caneMove �� true ������.
            stunmove.canMove = true;


        }
    }
}
