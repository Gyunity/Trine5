using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class BezierCurve_HMJ : MonoBehaviour
{
    public Vector3 point0, point1, point2, point3;

    // 베지어 곡선 점을 계산 - t(시간)에 따른 점 계산
    public Vector3 GetPoint(float t)
    {
        float u = 1 - t; // 곡선의 매개변수
        float tt = t * t; // t의 제곱
        float uu = u * u; // u의 제곱
        float uuu = uu * u; // u의 세제곱
        float ttt = tt * t; // t의 세제곱

        Vector3 p = uuu * point0;  // 시작점 기여도
        // (1 - t)^3 * p0
        p += 3 * uu * t * point1; // 첫 번째 제어점 기여도
        // 3 * (1-t)^2 * t * p1;
        p += 3 * u * tt * point2; // 두 번째 제어점 기여도
        // 3 * (1-t) * t^2 * p2
        p += ttt * point3; // 끝점 기여도
        // t^3 * p3
        return p;
    }

    public void SetPoint(Vector3 lastPosition)
    {
        point3 = lastPosition;
    }

    public void UpdatePoint()
    {
        point1 = (point0 + point3) * 0.3f;
        point1 = new Vector3(point1.x, point1.y + 5.0f, point1.z);
        point2 = new Vector3(point1.x, point1.y + 10.0f, point1.z);
    }
}
