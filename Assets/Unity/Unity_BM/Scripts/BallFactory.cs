using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public GameObject ballprefab;
    public float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(BallGo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(BallGo());
        }
    }
    IEnumerator BallGo()
    {
        for(int i=0; i<3; i++)
        {
            GameObject ball = Instantiate(ballprefab);
            ball.transform.position = transform.position;
            yield return new WaitForSeconds(delay);
        }
        
    }
}
