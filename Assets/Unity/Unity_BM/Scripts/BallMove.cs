using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    Rigidbody rd;
    public int movespeed = 10;

    //이펙트
    public GameObject explosionFactory;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rd.velocity += Vector3.right* movespeed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision other)
    {
        
        //만약에 부딪힌 오브젝트가 Player, Player weapon, wall 이면 
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.tag == "Player_equipment")
        {
            Debug.Log("볼 공격");
            //GameObject explosion = Instantiate(explosionFactory);
            //explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
        //삭제해라
        

        //그리고
    }

}
