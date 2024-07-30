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

    GameObject playerHead;

    Collider playerCollider;
    Collider targetCollider;

    Vector3 playerColliderCenter;

    PlayerState_HMJ playerState;
    float distanceX;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerState = player.GetComponent<PlayerState_HMJ>();

    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GrapplePoint"))
        {
            GameObject.Find("Player").GetComponent<PlayerState_HMJ>().SetState(PlayerState_HMJ.PlayerState.Grap);
            GameObject.Find("Player").GetComponent<PlayerMove_HMJ>().SetCollisionCollider(GetComponentInChildren<Collider>());

        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Vector3 direction = (swingPolePosition - player.transform.position).normalized;
        //player.transform.position += direction * swingSpeed * Time.deltaTime;
    }
}
