using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StungunMove : MonoBehaviour
{
    // Start is called before the first frame update
    // 이동해야 하는 거리
    float movedist;
    // 도착해야 하는 게임오브젝트
    public GameObject stungunPos;
    public float moveSpeed = 3f;

    // 움직일 수 있는가?
    public bool canMove;
    void Start()
    {
        // 현재 나의 위치에서 stungunPos 의 위치까지의 거리를 구하자.
        Vector3 dir = stungunPos.transform.position - transform.position;
        movedist = dir.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // 만약에 이동을 할 수 있다면
        if(canMove)
        {
            // 이동한 값만 큼 movedist 값을 빼자
            movedist -= Time.deltaTime;
            // movedis 가 0보다 작거나 같을 때 멈추자.
            if(movedist <= 0)
            {
                canMove = false;
            }
            // 아래로 움직이고 싶다.
            transform.position += Vector3.down * moveSpeed *Time.deltaTime;
        }
    }
}
