using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    public float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        //PlayerWeapon에도
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //공격
            Debug.Log("박스 공격");
            //GameObject explosion = Instantiate(explosionFactory);
            //explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
