using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePilarFactory : MonoBehaviour
{
    public GameObject firePilarPrefab;
    public float delay = 1f;
    public float padding = 2f;
    public int pilarCount = 8;
    public GameObject firePilarFactoryPos;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PilarGo());
        EnemyMove enemyMove = GetComponent<EnemyMove>();
        enemyMove.GoPilar = GoPilar;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(PilarGo());
        }

    }
    void GoPilar()
    {
        StartCoroutine(PilarGo());
    }

    IEnumerator PilarGo()
    {
        for (int i = 0; i < pilarCount; i++)
        {
            //firePilarPrefab 을 FirePilarFactory 위치로 옮긴다.
            //x값에 padding을 주면서 인스턴스을 생성한다. 
            Instantiate(firePilarPrefab, firePilarFactoryPos.transform.position + new Vector3(-i * padding, 0, 0), Quaternion.Euler(-90f,0f,0f));
            //pil.transform.position += transform.position;
            yield return new WaitForSeconds(delay);
        }
    }

}
