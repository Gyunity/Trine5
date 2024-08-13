using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject_HMJ : MonoBehaviour
{
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
        // 플레이어와 콜리전 충돌하면 삭제
        if(other.name.Contains("Player"))
            Destroy(gameObject);
    }
}
