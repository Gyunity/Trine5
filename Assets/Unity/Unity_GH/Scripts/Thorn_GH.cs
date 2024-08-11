using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn_GH : MonoBehaviour
{
    public GameObject popCone;
    public GameObject model;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            popCone.SetActive(true);
            model.SetActive(false);
            Destroy(other.gameObject);
            Destroy(gameObject, 5);
        }
    }
    
}
