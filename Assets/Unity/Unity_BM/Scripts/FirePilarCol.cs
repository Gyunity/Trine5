using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyMove;

public class FirePilarCol : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 1f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("뎀지");
        }
    }
   
}
