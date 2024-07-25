using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerSpeed = 5f;

    public GameObject bulletFactory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerFire();
    }

    void PlayerMove()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(h, v, 0);
        dir.Normalize();

        transform.position += dir * playerSpeed * Time.deltaTime;
    }
    void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletFactory);

            bullet.transform.position = transform.position;
        }
    }


}
