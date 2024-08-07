using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove_HMJ : MonoBehaviour
{
    float tValue = 0.0f;

    public BezierCurve_HMJ bezierCurve;
    float speed = 5.0f;

    public int numPoints = 4;
    private LineRenderer lineRenderer;

    Vector3 mousePos;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        bezierCurve = GetComponentInChildren<BezierCurve_HMJ>();

        speed = 5.0f;
    }
    // Start is called before the first frame update
    void Start()
    { 

    }

    // 화살 이동 경로 베지어 곡선으로 계산
    void MoveArrow()
    {
        // 0 ~ 1 사이로 t값 증가하도록 설정
        tValue += Time.deltaTime * speed;
        tValue = Mathf.Clamp01(tValue);
        Vector3 position = bezierCurve.GetPoint(tValue);
        transform.position = position;
    }

    // 화살 회전 방향
    void RotationArrow()
    {
        float nextData = tValue + Time.deltaTime * speed;

        // 1보다 클 경우 0 ~ 1까지만 존재해야하는 t 값이 문제가 생길 수 있어 강제 종료
        if (nextData > 1.0f)
            return;

        // 다음에 이동할 포지션 미리 계산
        Vector3 nextPosition = bezierCurve.GetPoint(nextData);
        // 목적지 - 출발지 (방향 구하기)
        Vector3 direction = nextPosition - transform.position;


        Vector3 myPos = transform.position;
        myPos.z = 0;

        Vector3 forwadVector = MouseClickPosition() - myPos;

        Vector3 upVector = Vector3.Cross(forwadVector, Vector3.right);


        // 해당 방향으로 로테이션 설정
        transform.rotation = Quaternion.LookRotation(forwadVector, upVector);


    }

    void DrawCurve()
    {
        //// 라인렌더러 점 개수를 50개로 고정
        //lineRenderer.positionCount = numPoints;

        //// numPoints만큼 개수 추가
        //Vector3[] positions = new Vector3[numPoints];

        //// t에 따른 포지션 저장
        //for (int i = 0; i < numPoints; i++)
        //{
        //    float t = (i / (float)(numPoints - 1)); // 점 개수에 따른 t의 값 조절
        //    positions[i] = bezierCurve.GetPoint(t);
        //    Debug.Log(i + ": " + positions[i].x + ", " + positions[i].y + ", " + positions[i].z);
        //}

        //lineRenderer.SetPositions(positions); // 라인 렌더러 포지션 셋팅


        ///////////////임시 테스트/////////////
        //힘, yVelocity, 출발 벡터, 땅에 닿았는 체크 할 bool
        bool isGround = false;

        Vector3 pos = transform.position;
        Vector3 dir = transform.forward * myPower;

        List<Vector3> linePositions = new List<Vector3>();

        linePositions.Add(pos);

        int test = 0;

        while (isGround == false)
        {
            Ray ray = new Ray(pos, dir);
            RaycastHit hitinfo;

            if(Physics.Raycast(ray, out hitinfo, dir.magnitude * Time.deltaTime))
            {
                isGround = true;
                linePositions.Add(hitinfo.point);
            }
            else
            {
                pos = pos + (Time.deltaTime * dir);
                linePositions.Add(pos);


                dir = dir + (Vector3.down * 9.8f * Time.deltaTime);

            }

            test++;

            if(test >= 500)
            {
                break;
            }
        }


        // 라인렌더러 점 개수를 50개로 고정
        lineRenderer.positionCount = linePositions.Count;

        // t에 따른 포지션 저장
        for (int i = 0; i < linePositions.Count; i++)
        {
            lineRenderer.SetPosition(i, linePositions[i]);
        }

        //SetPositions(positions); // 라인 렌더러 포지션 셋팅
    }

    // 마우스 클릭된 위치 반환
    Vector3 MouseClickPosition()
    {
        Vector3 mousePos = Input.mousePosition; // 현재 마우스 커서 가져오기
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; // 현재 z 값을 넣기 (2d -> 3d) 깊이 정보 추가
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos); // 마우스 좌표 -> 월드 좌표로 변경

        return new Vector3(worldPos.x, worldPos.y, 0.0f); // 레이케스팅해서 충돌된 오브젝트의 위치를 마우스 위치로 변경
    }

    Vector3 GetMousePos()
    {
        return mousePos;
    }

    void DestPosition()
    {
        // 마우스 레이케스트한 위치 Get - z값은 0으로 고정
        bezierCurve.SetPoint(MouseClickPosition());
        bezierCurve.UpdatePoint();
        tValue = 0.0f;
    }

    float myPower = 0;

    // Update is called once per frame
    void Update()
    {
        // 왼쪽 마우스 버튼을 클릭할 때
        if (Input.GetMouseButton(0)) 
        {
            // 화살 이동
            //MoveArrow();
            // 화살 해당 방향으로 회전
            RotationArrow();
            // 마우스를 클릭하면
            DestPosition();

            myPower += Time.deltaTime * 10f;

            myPower = Mathf.Clamp(myPower, 0, 50f);

            DrawCurve();
        }

        if(Input.GetMouseButtonUp(0))
        {
            myPower = 0;
        }
    }
}
