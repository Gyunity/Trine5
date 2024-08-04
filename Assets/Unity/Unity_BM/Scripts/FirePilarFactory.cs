using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePilarFactory : MonoBehaviour
{
    public GameObject firePilarPrefab;
    public float delay = 1f;
    public float padding = 2f;
    public int pilarCount = 8;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PilarGo());
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(PilarGo());
        }
    }

    IEnumerator PilarGo()
    {
        for (int i = 0; i < pilarCount; i++)
        {
            //firePilarPrefab 을 FirePilarFactory 위치로 옮긴다.
            //x값에 padding을 주면서 인스턴스을 생성한다. 
            Instantiate(firePilarPrefab, transform.position + new Vector3(-i * padding, 0, 0), Quaternion.identity);
            //pil.transform.position += transform.position;
            yield return new WaitForSeconds(delay);
        }
    }

}
