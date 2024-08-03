using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTri : MonoBehaviour
{
    public GameObject Cannon;
    Cannon_GH cannon_GH;
    void Start()
    {
        cannon_GH = Cannon.GetComponent<Cannon_GH>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //들어온 오브젝트가 Shell이면
        if(other.gameObject.layer == LayerMask.NameToLayer("SummonedObject"))
        {
            //shell을 2초뒤에 파괴한다.
            Destroy(other.gameObject);
            cannon_GH.shellLoad = true;
        }
    }
}
