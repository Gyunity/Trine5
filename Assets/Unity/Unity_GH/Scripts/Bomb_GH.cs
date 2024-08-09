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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bombCurrTime += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (Input.GetKeyDown(KeyCode.N))
        {
            fireMode = false;
        }

        if (bombCurrTime > bombTime && fireMode)
        {
            GameObject bombEff = Instantiate(bombEffect);
            bombEff.transform.position = transform.position;

            Collider[] cannonShields = Physics.OverlapSphere(transform.position, 3);

            foreach (Collider canonShield in cannonShields)
            {
                if (canonShield.transform.gameObject.layer == LayerMask.NameToLayer("CannonShield"))
                {
                    canonShield.transform.gameObject.SetActive(false);
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
        //얼음 화살로 변경
        //if (collision.gameObject)
        //{
        //    fireMode = false;
        //}
    }
}
