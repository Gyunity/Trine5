using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFactory : MonoBehaviour
{
    public GameObject boxPrefab;
    public float delay = 1f;
    public int boxCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(BoxGo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(BoxGo());
        }
    }
    IEnumerator BoxGo()
    {
        
        //박스를 랜덤한 위치에 위치한다.
        for (int i = 0; i < boxCount; i++)
        {
            int rd = Random.Range(1, 10);
            Instantiate(boxPrefab, new Vector3(rd, this.transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(delay);
            rd = 0;
        }
    }
}
