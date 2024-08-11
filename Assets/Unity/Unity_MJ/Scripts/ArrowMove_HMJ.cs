using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static ArrowMove_HMJ;
using static UnityEditor.PlayerSettings;

public class ArrowMove_HMJ : MonoBehaviour
{
    public enum ArrowState
    {
        ArrowMove,
        ArrowDirection,
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
    public int moveIndexAdd = 2;

    public ArrowState m_eCurArrowState;
    public ArrowState m_ePreArrowState;

    public float moveTime = 0.0f;

    public float myPower = 0;

    float powerSpeed = 50.0f;

    public GameObject arrowObject;

    float lifeTime = 0.0f;

    Animator curAnim;

    float smoothStep = 0.0f;
    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        bezierCurve = GetComponentInChildren<BezierCurve_HMJ>();
        speed = 5.0f;

        m_eCurArrowState = ArrowState.ArrowStateEnd;
        m_ePreArrowState = ArrowState.ArrowStateEnd;

        lifeTime = 3.0f;

        smoothStep = -0.3f;

        arrowObject = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "ArrowPosition").transform.gameObject;
    }

    void ArrowMove()
    {
        transform.position += dir * speed;
        //Debug.Log("화살 dir: " + dir.x + ", " + dir.y + ", " + dir.z);
        //Debug.Log("화살 이동 중~: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);22
    }

    void UpdateState()
    {
        switch (m_eCurArrowState)
        {
            case ArrowState.ArrowMove:
                {
                    MoveArrow();
                    ArrowMove();
                }
                break;
            case ArrowState.ArrowDirection:
                {
                }
                break;
            case ArrowState.ArrowDraw:
                {
                    transform.position = arrowObject.transform.position;
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
    public void ChangeState(ArrowState arrowState)
    {
        if (m_eCurArrowState != arrowState)
        {
            switch (arrowState)
            {
                case ArrowState.ArrowMove:
                    {
                        //curAnim.SetTrigger("ArrowShoot");
                        Debug.Log("화살 위치: 현재 화살 위치 설정");
                        moveIndex = 0;
                        dir = new Vector3(0.0f, 0.0f, 0.0f);
                        myPower = 0;
                        lineRenderer.enabled = false;
                    }
                    break;
                case ArrowState.ArrowDraw:
                    {

                        //curAnim.SetTrigger("ArrowDraw");
                    }
                    break;
                case ArrowState.ArrowDirection:
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
        if (moveIndex + 2 > linePositions.Count)
        {
            dir = new Vector3(0.0f, 0.0f, 0.0f);
            return;
        }
            
        dir = (linePositions[moveIndex] - transform.position).normalized;

        //Debug.Log("Dir: " + dir.x + ", " + dir.y + ", " + dir.z);
        // 현재 위치가 목표 지점보다 오른쪽에 있으면 다음 인덱스로 

        if(Vector3.Distance(transform.position, linePositions[moveIndex + 1]) + smoothStep <= Vector3.Distance(linePositions[moveIndex], linePositions[moveIndex + 1]))
        {
            // 방향 바꾸는 것을 이후 궤적 포인트로 계산 (2번째 도달지 - 1번째 도달지).normalized
            transform.rotation = Quaternion.LookRotation((linePositions[moveIndex + 1] - linePositions[moveIndex]).normalized);

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

    //void DrawReflection()
    //{
    //    linePositions.Clear();

    //    Vector3 position = transform.position;
    //    Vector3 velocity = new Vector3(0.0f, 9.8f, 0.0f);

    //    for (int i = 0; i < 100; i++)
    //    {
    //        lineRenderer.SetPosition(i, position);

    //        // 물리적 시뮬레이션 (이 예제에서는 단순화된 물리 계산 사용)
    //        position += velocity * Time.fixedDeltaTime;

    //        // 반사 처리
    //        if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, velocity.magnitude * Time.fixedDeltaTime))
    //        {
    //            Vector3 normal = hit.normal;
    //            velocity = Vector3.Reflect(velocity, normal);

    //            // 궤적이 너무 길어지면 멈춤
    //            if (i >= 100 - 1) break;
    //        }

    //        // 단순 중력 적용 (옵션)
    //        velocity += Physics.gravity * Time.fixedDeltaTime;
    //    }
    //}


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
                Vector3 hitPos = new Vector3(hitinfo.point.x, hitinfo.point.y, 0.0f);
                linePositions.Add(hitPos);
            }
            else
            {
                pos.z = 0.0f;
                dir.z = 0.0f;
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

        myPower += Time.deltaTime * powerSpeed;
 
        myPower = Mathf.Clamp(myPower, 0, 90f);

        DrawTrajectory();
        //DrawCurve();
    }

    void ResetArrowValue()
    {
        myPower = 0.0f;
        moveIndex = 0;

        m_eCurArrowState = ArrowState.ArrowStateEnd;
        m_ePreArrowState = ArrowState.ArrowStateEnd;

        speed = 0.1f;

        numPoints = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && (m_eCurArrowState != ArrowState.ArrowDraw) && (m_eCurArrowState != ArrowState.ArrowMove))
        {
            ResetArrowValue();
            ChangeState(ArrowState.ArrowDraw);
        }

        if (Input.GetMouseButtonUp(0) && (m_eCurArrowState == ArrowState.ArrowDraw) && (m_eCurArrowState != ArrowState.ArrowMove))
            ChangeState(ArrowState.ArrowMove);

        UpdateState();
    }
}
