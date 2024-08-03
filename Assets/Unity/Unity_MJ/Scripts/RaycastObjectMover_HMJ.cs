using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class RaycastObjectMover_HMJ : MonoBehaviour
{
    GameObject hitObject;

    float rotateValue = 0.0f;
    float rotateSpeed = 50.0f;
    bool mouseClick = false;
    private void FixedUpdate()
    {
        // 마우스 오른쪽 버튼을 클릭했을 때만 해당되는 레이어 오브젝트의 위치가 변경되도록 수정
        ObjectMove();
    }

    void ObjectMove()
    {
        RaycastHit hitInfo;
        if(Input.GetMouseButton(0)) // 계속 마우스를 누르고 있고
        {
            if ((null == hitObject) && RaycastGrab(out hitInfo)) // 만약 마우스와 충돌된 오브젝트가 이전에 없었고, 현재 있다면 저장
                hitObject = hitInfo.collider.gameObject;
            else if (hitObject)
            {
                RaycastMove(); // 현재 hit된 보관된 오브젝트를 마우스 위치로 변경(2d -> 3d) (만약 레이케스팅된 물체가 있다면 실행)
                RotateMove();
                if (Input.GetKey(KeyCode.Z))
                    rotateValue += Time.deltaTime * rotateSpeed;
                else if(Input.GetKey(KeyCode.X))
                    rotateValue -= Time.deltaTime * rotateSpeed;

            }
        }
        else if(hitObject) // 마우스를 누르지 않을 경우 다시 충돌된 오브젝트를 null로 넣어줌. (해당 정보로 클릭 여부 확인)
        {
            hitObject.GetComponentInChildren<Rigidbody>().useGravity = true;
            hitObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
            hitObject = null;
        }

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

    void RaycastMove()
    {
        Vector3 mousePos = Input.mousePosition; // 현재 마우스 커서 가져오기
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; // 현재 z 값을 넣기 (2d -> 3d) 깊이 정보 추가
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos); // 마우스 좌표 -> 월드 좌표로 변경
        hitObject.transform.position = new Vector3(worldPos.x, worldPos.y, 0.0f); // 레이케스팅해서 충돌된 오브젝트의 위치를 마우스 위치로 변경

        hitObject.GetComponentInChildren<Rigidbody>().useGravity = false;
        hitObject.GetComponentInChildren<Rigidbody>().isKinematic = true;
    }

    void RotateMove()
    {
        hitObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, rotateValue);
    }
}
