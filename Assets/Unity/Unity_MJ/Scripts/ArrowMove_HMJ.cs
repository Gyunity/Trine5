using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static ArrowMove_HMJ;

public class ArrowMove_HMJ : MonoBehaviour
{
    public enum ArrowState
    {
        ArrowMove,
        ArrowNon,
        ArrowDraw,
        ArrowStateEnd
    }

    public float tValue = 0.0f;

    public BezierCurve_HMJ bezierCurve;
    public float speed;

    public int numPoints = 4;
    public LineRenderer lineRenderer;

    public Vector3 mousePos;

    public Vector3 dir;

    public List<Vector3> linePositions = new List<Vector3>();
    public int moveIndex = 0;

    public ArrowState m_eCurArrowState;
    public ArrowState m_ePreArrowState;

    public float moveTime = 0.0f;

    public float myPower = 0;

    public GameObject arrowObject;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        bezierCurve = GetComponentInChildren<BezierCurve_HMJ>();
        speed = 0.1f;

        m_eCurArrowState = ArrowState.ArrowStateEnd;
        m_ePreArrowState = ArrowState.ArrowStateEnd;
    }

    void ArrowMove()
    {
        transform.position += dir * speed;
        //Debug.Log("화살 dir: " + dir.x + ", " + dir.y + ", " + dir.z);
        //Debug.Log("화살 이동 중~: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
    }

    void UpdateState()
    {
        switch (m_eCurArrowState)
        {
            case ArrowState.ArrowMove:
                {
                    //RotationArrow();
                    MoveArrow();
                    ArrowMove();
                }
                break;
            case ArrowState.ArrowNon:
                {
                }
                break;
            case ArrowState.ArrowDraw:
                {
                    linePositions.Clear();
                    LineRender_On();
                }
                break;
            case ArrowState.ArrowStateEnd:
                {

                }
                break;

        }
    }
        void ChangeState(ArrowState arrowState)
    {
        if (m_eCurArrowState != arrowState)
        {
            switch (arrowState)
            {
                case ArrowState.ArrowMove:
                    {
                        transform.position = arrowObject.transform.position;
                        Debug.Log("화살 위치: 현재 화살 위치 설정");
                        moveIndex = 0;
                        dir = new Vector3(0.0f, 0.0f, 0.0f);
                        //transform.position = ;
                        myPower = 0;
                        lineRenderer.enabled = false;
                    }
                    break;
                case ArrowState.ArrowDraw:
                    {
                    }
                    break;
                case ArrowState.ArrowStateEnd:
                    {
                    }
                    break;

            }

            m_ePreArrowState = m_eCurArrowState;
            m_eCurArrowState = arrowState;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void MoveArrow()
    {
        if (moveIndex + 1 > linePositions.Count)
        {
            dir = new Vector3(0.0f, 0.0f, 0.0f);
            return;
        }
            

        dir = (linePositions[moveIndex] - transform.position).normalized;

        //Debug.Log("Dir: " + dir.x + ", " + dir.y + ", " + dir.z);
        // 현재 위치가 목표 지점보다 오른쪽에 있으면 다음 인덱스로 

        transform.rotation = Quaternion.LookRotation(dir);

        if (Vector3.Distance(linePositions[moveIndex], transform.position) < 1.0f)
        {
            moveIndex++;
            print(moveIndex);
        }
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
        linePositions.Clear();

        bool isGround = false;

        Vector3 pos = arrowObject.transform.position;
        Vector3 dir = transform.forward * myPower;

        linePositions.Add(pos);

        int test = 0;

        while (isGround == false)
        {
            Ray ray = new Ray(pos, dir);
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, dir.magnitude * Time.deltaTime))
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
            if (test >= 500)
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

    void LineRender_On()
    {
        Debug.Log("라인 렌더러 활성화o");
        lineRenderer.enabled = true;
        // 화살 이동
        MoveArrow();
        // 화살 해당 방향으로 회전
        RotationArrow();

        myPower += Time.deltaTime * 10f;

        myPower = Mathf.Clamp(myPower, 0, 50f);

        DrawCurve();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && (m_eCurArrowState != ArrowState.ArrowDraw))
        {
            ChangeState(ArrowState.ArrowDraw);
        }

        if (Input.GetMouseButtonUp(0) && (m_eCurArrowState != ArrowState.ArrowMove))
        {
            ChangeState(ArrowState.ArrowMove);
        }

        UpdateState();
    }
}
