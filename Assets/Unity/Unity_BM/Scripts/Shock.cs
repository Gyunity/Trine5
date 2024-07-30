using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour
{
    //폭파반경
    public float exploRange = 3f;
    public string target = "Player";

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
        //나의 중심에서 반경 3미터 안에 있는 Obstacle 들을 검색하자.
        Collider[] colliders = Physics.OverlapSphere(transform.position, exploRange);//, 1<<LayerMask.NameToLayer("Obstacle")
        //검색한 물체를 모두 파괴하자
        for (int i = 0; i < colliders.Length; i++)
        {
            //만약에 검색된 물체의 Layerrk obstacle이 아니면 continue 하겠다.
            if (colliders[i].gameObject.layer != LayerMask.NameToLayer(target)) continue;
            Destroy(colliders[i].gameObject);
        }
    }
}
