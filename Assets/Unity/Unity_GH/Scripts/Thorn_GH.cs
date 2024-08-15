using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn_GH : MonoBehaviour
{
    public GameObject popCone;
    public GameObject model;

    BoxCollider boxcoll;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        boxcoll = GetComponent<BoxCollider>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow") || collision.gameObject.layer == LayerMask.NameToLayer("PlayerSword"))
        {
        SoundManager.instance.PlayBossEftSound(SoundManager.EBossEftType.BOSS_TRAPHIT);

            popCone.SetActive(true);
            model.SetActive(false);
            boxcoll.enabled = false;
            Destroy(collision.gameObject);
            Destroy(gameObject, 5);
        }
    }


}
