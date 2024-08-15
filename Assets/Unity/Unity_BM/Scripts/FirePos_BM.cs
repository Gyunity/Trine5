using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePos_BM : MonoBehaviour
{
    public GameObject firePos;
    public float delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BallGoCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator BallGoCo()
    {
        for (int i = 0; i < 3; i++)
        {
        GameObject ball = Instantiate(firePos);
        ball.transform.position = transform.position;
        //this.transform.position;

        yield return new WaitForSeconds(delay);
        }

    }
}
