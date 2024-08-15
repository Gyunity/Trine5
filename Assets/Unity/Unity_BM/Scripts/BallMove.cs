using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    Rigidbody rd;
    public int movespeed = 10;

    //이펙트
    public GameObject explosionFactory;

    GameObject player;
    //hp 스크립트 받기 (to do)
    HPSystem_HMJ playerHP;


    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerHP = player.GetComponent<HPSystem_HMJ>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rd.velocity += Vector3.left* movespeed * Time.deltaTime;
    }
    //public Vector3 knockBack = Vector3.zero;

    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject == player)
        {
            print("양배추 플레이어 -10");
            playerHP.UpdateHP(-300, GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType());
            Destroy(gameObject);
        }


        ////만약에 부딪힌 오브젝트가 Player, Player weapon, wall 이면 
        //if ( other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.tag == "Player_equipment")
        //{
        //    Debug.Log("볼 공격");
        //    //GameObject explosion = Instantiate(explosionFactory);
        //    //explosion.transform.position = transform.position;
        //    Destroy(gameObject);
        //}

        //if (other.gameObject.tag == "Player")
        //{
        //    //플레이어 hp에 데미지
        //    print("양배추 플레이어 -10");
        //    //Vector3 delivereKnockBack = transform.localScale.x > 0 ? knockBack : new Vector3(-knockBack.x, knockBack.y, 0);
            
        //    //player.transform. -= Vector3.forward * 3;
        //    Destroy(gameObject);
        //}
        
        //삭제해라


        //그리고
    }

}
