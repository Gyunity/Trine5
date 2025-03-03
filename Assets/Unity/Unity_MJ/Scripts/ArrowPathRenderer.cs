using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPathRenderer : MonoBehaviour
{
    List<Vector3> linePositions = new List<Vector3>();
    public float myPower = 0;
    public LineRenderer lineRenderer;
    float powerSpeed = 50.0f;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void DrawTrajectory()
    {
        Vector3 pos = transform.position;
        Vector3 dir = transform.forward * myPower;

        int reflections = 0;

        // 레이어 이름을 기반으로 레이어 인덱스 얻기
        int layerIndex = LayerMask.NameToLayer("ArrowReflection");

        // 레이어 마스크를 설정 (단일 레이어)
        LayerMask collisionMask = 1 << layerIndex;

        linePositions.Clear();
        linePositions.Add(pos);

        while (true)
        {
            if ((reflections > 50) || (linePositions.Count > 500))
                break;
            Ray ray = new Ray(pos, dir);
            RaycastHit hitinfo;

            // 레이어 마스크를 사용하여 특정 레이어와만 충돌
            if (Physics.Raycast(ray, out hitinfo, dir.magnitude * Time.deltaTime, collisionMask))
            {
                linePositions.Add(hitinfo.point);
                dir = Vector3.Reflect(dir, hitinfo.normal); // 반사 처리
                pos = hitinfo.point;
                linePositions.Add(pos);
                reflections++;
                powerSpeed = 100.0f;
            }
            else
            {
                pos += dir * Time.deltaTime;
                dir += Vector3.down * 9.8f * Time.deltaTime; // 중력 적용
                linePositions.Add(pos);
            }
        }

        // 라인렌더러 점 개수를 최대 포인트 수로 조정
        lineRenderer.positionCount = Mathf.Min(linePositions.Count, 500);

        // 궤적 포지션 저장
        for (int i = 0; i < lineRenderer.positionCount; i++)
            lineRenderer.SetPosition(i, linePositions[i]);
    }

    public void LineRender_On()
    {
        //Debug.Log("라인 렌더러 활성화o");
        lineRenderer.enabled = true;
        // 화살 이동
         // MoveArrow();
        // 화살 해당 방향으로 회전
         // RotationArrow();

        myPower += Time.deltaTime * powerSpeed;

        myPower = Mathf.Clamp(myPower, 0, 90f);

        DrawTrajectory();
    }

    public void LineRender_Off()
    {
        //Debug.Log("라인 렌더러 활성화x
        lineRenderer.enabled = false;
    }

    public void LineClear()
    {
        linePositions.Clear();
    }

    public List<Vector3> GetLinePositions()
    {
        return linePositions;
    }

}
