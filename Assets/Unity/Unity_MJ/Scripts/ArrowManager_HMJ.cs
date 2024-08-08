using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager_HMJ : MonoBehaviour
{
    public GameObject arrowPrefab; // 인스펙터에서 설정할 화살 프리팹

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnArrow()
    {
        Instantiate(arrowPrefab);
    }
}
