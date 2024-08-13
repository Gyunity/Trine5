using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWayPoint_HMJ : MonoBehaviour
{
    public Transform[] waypoints;  // 경로를 나타내는 점들
    public float speed = 5f;       // 이동 속도

    private int currentWaypointIndex = 0;

    Vector3 moveDirection;
    Vector3 moveleftDirection;
    GameObject player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
                Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        moveDirection = (targetPosition - player.transform.position).normalized * 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0)
            return;


        UpdateNextWayPoint();


        if (Input.GetKey(KeyCode.RightArrow))
        {
            UpdatePreWayPoint();
        }

    }

    void UpdateNextWayPoint()
    {
        // 현재 위치와 다음 지점 사이의 y df
        // 현재 위치와 다음 지점 사이의 벡터 계산
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        moveDirection = (targetPosition - player.transform.position).normalized * 5.0f;

        // 지점에 도착하면 다음 지점으로 전환
        Vector3 playerPosition = player.transform.position;
        playerPosition.y = 0.0f;
        targetPosition.y = 0.0f;

        Debug.Log("현재 웨이 포인트 방향: " + moveDirection);

        if (Vector3.Distance(playerPosition, targetPosition) < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            Debug.Log("웨이 포인트 변경: " + currentWaypointIndex);
        }
    }

    void UpdatePreWayPoint()
    {
        if (currentWaypointIndex - 1 < 0)
            return;
        // 현재 위치와 다음 지점 사이의 y df
        // 현재 위치와 다음 지점 사이의 벡터 계산
        Vector3 targetPosition = waypoints[currentWaypointIndex - 1].position;
        moveleftDirection = (targetPosition - player.transform.position).normalized * 5.0f;

        // 지점에 도착하면 다음 지점으로 전환
        Vector3 playerPosition = player.transform.position;
        playerPosition.y = 0.0f;
        targetPosition.y = 0.0f;

        Debug.Log("현재 웨이 포인트 방향: " + moveleftDirection);

        if (Vector3.Distance(playerPosition, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex - 1) % waypoints.Length;
            Debug.Log("웨이 포인트 변경: " + currentWaypointIndex);
        }
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public Vector3 GetPreMoveDirection()
    {
        return moveleftDirection;
    }
}
