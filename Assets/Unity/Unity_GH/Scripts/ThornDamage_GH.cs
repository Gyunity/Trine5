using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornDamage_GH : MonoBehaviour
{
    GameObject player;
    HPSystem_HMJ playerHP;


    void Start()
    {
        player = GameObject.Find("Player");
        playerHP = player.GetComponent<HPSystem_HMJ>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerHP.UpdateHP(-10, GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType());
        }
    }
}
