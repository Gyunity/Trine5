using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static ArrowMove_HMJ;
using static ArrowType_HMJ;
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

    public LineRenderer lineRenderer;

    public Vector3 dir;

    public List<Vector3> linePositions = new List<Vector3>();
    public int moveIndex = 0;

    public ArrowState m_eCurArrowState;
    public ArrowState m_ePreArrowState;

    public float myPower = 0;

    float powerSpeed = 50.0f;

    public GameObject arrowObject;

    float smoothStep = 0.0f;

    ArrowType_HMJ arrowTypeData;

    public ArrowType arrowType;

    EffectManager_HMJ effectManager;

    GameObject effectObject;

    Transform childTransform;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        bezierCurve = GetComponentInChildren<BezierCurve_HMJ>();
        speed = 5.0f;

        m_eCurArrowState = ArrowState.ArrowStateEnd;
        m_ePreArrowState = ArrowState.ArrowStateEnd;

        smoothStep = -0.3f;
        arrowTypeData = GameObject.Find("ArrowManager").GetComponentInChildren<ArrowType_HMJ>();


        arrowObject = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "ArrowPosition").transform.gameObject;
        arrowType = arrowTypeData.GetArrowType();

        effectManager = GetComponentInChildren<EffectManager_HMJ>();

        childTransform = transform.Find("ArrowPosition");
    }

    void ArrowMove()
    {
        transform.position += dir * speed;
    }

    void UpdateState()
    {
        switch (m_eCurArrowState)
        {
            case ArrowState.ArrowMove:
                {
                    MoveArrow();
                    ArrowMove();
                    if (effectObject)
                        effectObject.transform.position = childTransform.position;
                }
                break;
            case ArrowState.ArrowDraw:
                {
                    transform.position = arrowObject.transform.position;
                    linePositions.Clear();
                    LineRender_On();
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
                        Debug.Log("화살 위치: 현재 화살 위치 설정");
                        moveIndex = 0;
                        dir = new Vector3(0.0f, 0.0f, 0.0f);
                        myPower = 0;
                        lineRenderer.enabled = false;

                        switch (arrowType)
                        {
                            case ArrowType.ArrowIceType:
                                effectObject = effectManager.SpawnAndPlayEffect(childTransform.position, 10.0f, false, new Vector3(0.0f, 0.0f, 0.0f));
                                break;
                            case ArrowType.ArrowFireType:
                                effectObject = effectManager.SpawnAndPlayEffect2(childTransform.position, 10.0f, false, new Vector3(0.0f, 0.0f, 0.0f));
                                break;
                        }
                    }
                    break;
                case ArrowState.ArrowStateEnd:
                    break;
            }

            m_ePreArrowState = m_eCurArrowState;
            m_eCurArrowState = arrowState;
        }
    }

    void MoveArrow()
    {
        if (moveIndex + 2 > linePositions.Count)
        {
            dir = new Vector3(0.0f, 0.0f, 0.0f);
            return;
        }
            
        dir = (linePositions[moveIndex] - transform.position).normalized;

        if(Vector3.Distance(transform.position, linePositions[moveIndex + 1]) + smoothStep <= Vector3.Distance(linePositions[moveIndex], linePositions[moveIndex + 1]))
        {
            // 방향 바꾸는 것을 이후 궤적 포인트로 계산 (2번째 도달지 - 1번째 도달지).normalized
            transform.rotation = Quaternion.LookRotation((linePositions[moveIndex + 1] - linePositions[moveIndex]).normalized);

            moveIndex++;
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
    void LineRender_On()
    {
        lineRenderer.enabled = true;
        MoveArrow();
        RotationArrow();

        myPower += Time.deltaTime * powerSpeed;
        myPower = Mathf.Clamp(myPower, 0, 90f);

        DrawTrajectory();
    }

    void ResetArrowValue()
    {
        myPower = 0.0f;
        moveIndex = 0;

        m_eCurArrowState = ArrowState.ArrowStateEnd;
        m_ePreArrowState = ArrowState.ArrowStateEnd;

        speed = 0.1f;
    }

    void UpdateArrow()
    {
        if (Input.GetMouseButton(0) && (m_eCurArrowState != ArrowState.ArrowDraw) && (m_eCurArrowState != ArrowState.ArrowMove))
        {
            ResetArrowValue();
            ChangeState(ArrowState.ArrowDraw);
        }

        if (Input.GetMouseButtonUp(0) && (m_eCurArrowState == ArrowState.ArrowDraw) && (m_eCurArrowState != ArrowState.ArrowMove))
            ChangeState(ArrowState.ArrowMove);
    }
    void Update()
    {
        UpdateArrow();
        UpdateState();
    }
}
