using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static ArrowMove_HMJ;
using static ArrowType_HMJ;
using static ArrowPathRenderer;
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

    public ArrowPathRenderer arrowPathRender = null;

    public BezierCurve_HMJ bezierCurve;
    public float speed;

    public Vector3 dir;

    int moveIndex = 0;

    public ArrowState m_eCurArrowState;
    public ArrowState m_ePreArrowState;

    public GameObject arrowObject;
    public ArrowType arrowType;

    public float myPower = 0;
    float smoothStep = 0.0f;

    EffectManager_HMJ effectManager;
    GameObject effectObject;

    Transform childTransform;

    private void Awake()
    {
        bezierCurve = GetComponentInChildren<BezierCurve_HMJ>();
        speed = 5.0f;

        m_eCurArrowState = ArrowState.ArrowStateEnd;
        m_ePreArrowState = ArrowState.ArrowStateEnd;

        smoothStep = -0.3f;

        arrowObject = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "ArrowPosition").transform.gameObject;
        arrowType = GameObject.Find("ArrowManager").GetComponentInChildren<ArrowType_HMJ>().GetArrowType();

        effectManager = GetComponentInChildren<EffectManager_HMJ>();

        childTransform = transform.Find("ArrowPosition");
    }

    void ArrowMove()
    {
        transform.position += dir * speed;
    }

    void MoveArrow()
    {
        List<Vector3> linePositions = arrowPathRender.GetLinePositions();
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
        float nextData = Mathf.Clamp(Time.deltaTime * speed, 0.0f, 1.0f);
        // 1보다 클 경우 0 ~ 1까지만 존재해야하는 t 값이 문제가 생길 수 있어 강제로 변환

        Vector3 myPos = transform.position;
        myPos.z = 0;

        Vector3 forwadVector = MouseClickPosition() - myPos;
        Vector3 upVector = Vector3.Cross(forwadVector, Vector3.right);

        // 해당 방향으로 로테이션 설정
        transform.rotation = Quaternion.LookRotation(forwadVector, upVector);
    }

    // 마우스 클릭된 위치 반환
    Vector3 MouseClickPosition()
    {
        Vector3 mousePos = Input.mousePosition; // 현재 마우스 커서 가져오기
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; // 현재 z 값을 넣기 (2d -> 3d) 깊이 정보 추가
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos); // 마우스 좌표 -> 월드 좌표로 변경

        return new Vector3(worldPos.x, worldPos.y, 0.0f); // 레이케스팅해서 충돌된 오브젝트의 위치를 마우스 위치로 변경
    }

    void ResetArrowValue()
    {
        myPower = 0.0f; 
        moveIndex = 0;

        m_eCurArrowState = ArrowState.ArrowStateEnd;
        m_ePreArrowState = ArrowState.ArrowStateEnd;

        speed = 0.1f;
    }

    public void triggerArrowShot()
    {
        if (Input.GetMouseButton(0) && (m_eCurArrowState != ArrowState.ArrowDraw) && (m_eCurArrowState != ArrowState.ArrowMove))
        {
            ResetArrowValue();
            ChangeState(ArrowState.ArrowDraw);
        }

        if (Input.GetMouseButtonUp(0) && (m_eCurArrowState == ArrowState.ArrowDraw) && (m_eCurArrowState != ArrowState.ArrowMove))
            ChangeState(ArrowState.ArrowMove);
    }


    // State 관련 부분
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
                    arrowPathRender.LineClear();
                    arrowPathRender.LineRender_On();
                    RotationArrow();
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
                        arrowPathRender.LineRender_Off();

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
            }
            m_ePreArrowState = m_eCurArrowState;
            m_eCurArrowState = arrowState;
        }
    }

    void Update()
    {
        triggerArrowShot();
        UpdateState();
    }
}
