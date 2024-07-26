using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerGrab_HMJ : MonoBehaviour
{
    GameObject player;
    float swingSpeed = 5.0f;
    bool isSwinging = false;
    Vector3 swingPolePosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if(isSwinging) // ���� ��� �ִٸ�
        //{

        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹! �÷��̾� �հ�: " + other.name);
       
        if (other.gameObject.layer == LayerMask.NameToLayer("GrapplePoint"))
        {
            GameObject.Find("Player").GetComponent<PlayerState_HMJ>().SetState(PlayerState_HMJ.PlayerState.Grap);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Vector3 direction = (swingPolePosition - player.transform.position).normalized;
        //player.transform.position += direction * swingSpeed * Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�浹 �� Exit: " + other.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("GrapplePoint"))
        {
            PlayerState_HMJ playerState = player.GetComponent<PlayerState_HMJ>();
            // ���� �浹���� �� ���� ������ ����
            if (player)
                playerState.SetState(PlayerState_HMJ.PlayerState.GrapDown);
        }
    }
}
