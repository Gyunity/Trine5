using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RaycastObjectMover_HMJ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // 마우스 오른쪽 버튼을 클릭했을 때만 해당되는 레이어 오브젝트의 위치가 변경되도록 수정
        ObjectMove();
    }

    void ObjectMove()
    {
        if (Input.GetMouseButton(1) == false) return; // 햔재 오른쪽 마우스 클릭x -> false 반환

        RaycastHit hitInfo;
        if(RaycastGrab(out hitInfo)) // 현재 충돌 여부 및 부딪힌 hit 정보 가져오기
            RaycastMove(hitInfo); // 현재 hit된 오브젝트를 마우스 위치로 변경(2d -> 3d) (만약 레이케스팅된 물체가 있다면 실행)
    }

    bool RaycastGrab(out RaycastHit hitInfo)
    {

        // 화면 좌표에 ray 만들기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Raycast를 이용해서 부딪힌 정보를 얻어오자.

        // 일단 사용자 소환 물체만 가능하게
        if(Physics.Raycast(ray, out hitInfo, float.MaxValue, 1 << LayerMask.NameToLayer("SummonedObject")))
        {
            return true; // 해당 레이어의 물체와 충돌했으면 true 반환
        }

        return false; // 해당 레이어의 물체와 충돌하지 않았으면 false 반환
    }

    void RaycastMove(RaycastHit hitInfo)
    {
        Vector3 mousePos = Input.mousePosition; // 현재 마우스 커서 가져오기
        mousePos.z = Camera.main.WorldToScreenPoint(hitInfo.transform.position).z; // 현재 z 값을 넣기 (2d -> 3d) 깊이 정보 추가
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos); // 마우스 좌표 -> 월드 좌표로 변경
        hitInfo.collider.gameObject.transform.position = worldPos; // 레이케스팅해서 충돌된 오브젝트의 위치를 마우스 위치로 변경
    }
}
