using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_GH : MonoBehaviour
{
    float bombCurrTime = 0;
    float bombTime = 5;

    public GameObject bombEffect;
    public GameObject bombFireEffect;

    public GameObject bombRange;

    bool fireMode = true;
    bool bombDamage = true;

    void Start()
    {

    }

    void Update()
    {
        bombCurrTime += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    fireMode = false;
        //}

        if (bombCurrTime > bombTime && fireMode)
        {
            GameObject bombEff = Instantiate(bombEffect);
            bombEff.transform.position = transform.position;

            Collider[] BombRanges = Physics.OverlapSphere(transform.position, 5);

            foreach (Collider bombAttack in BombRanges)
            {
                if (bombAttack.transform.gameObject.layer == LayerMask.NameToLayer("CannonShield"))
                {
                    bombAttack.transform.gameObject.SetActive(false);
                }

                if(bombAttack.transform.gameObject.layer == LayerMask.NameToLayer("Player") && bombDamage)
                {
                    HPSystem_HMJ playerHP = bombAttack.transform.gameObject.GetComponent<HPSystem_HMJ>();
                    playerHP.UpdateHP(-500);
                    bombDamage = false;
                }
            }

            Destroy(gameObject);
            
        }

        if (!fireMode)
        {
            gameObject.tag = "Shell";
            bombRange.SetActive(false);
            bombFireEffect.SetActive(false);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            fireMode = false;
        }
    }
}
