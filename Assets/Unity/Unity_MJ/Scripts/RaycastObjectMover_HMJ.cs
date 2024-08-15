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

    GameObject player;
    ChangeCharacter changeCharacter;

    GameObject effectManagerObject;
    EffectManager_HMJ effectManager;

    public GameObject wizardRightHand;
    public GameObject wizardLeftHand;

    GameObject effect;

    public GameObject wizardRightEffect;
    public GameObject wizardLeftEffect;

    private void Start()
    {
        player = GameObject.Find("Player");
        changeCharacter = player.GetComponentInChildren<ChangeCharacter>();
        effectManagerObject = GameObject.Find("RaycastObjectEffect");
        effectManager = effectManagerObject.GetComponentInChildren<EffectManager_HMJ>();

        wizardRightEffect = effectManager.SpawnAndPlayEffect3(new Vector3(0.0f, 0.0f, 0.0f), 500000000.0f, false, new Vector3(0.0f, 0.0f, 0.0f));
        wizardRightEffect.SetActive(false);

        wizardLeftEffect = effectManager.SpawnAndPlayEffect3(new Vector3(0.0f, 0.0f, 0.0f), 500000000.0f, false, new Vector3(0.0f, 0.0f, 0.0f));
        wizardLeftEffect.SetActive(false);
    }
    private void FixedUpdate()
    {
        // 마우스 오른쪽 버튼을 클릭했을 때만 해당되는 레이어 오브젝트의 위치가 변경되도록 수정
        if (changeCharacter.GetPlayerCharacterType() == PlayerCharacterType.WizardType)
        {


            ObjectMove();
        }
            
    }

    public void SetHandData()
    {

    }

    void ObjectMove()
    {
        RaycastHit hitInfo;
        if(Input.GetMouseButton(0)) // 계속 마우스를 누르고 있고
        {
            if ((null == hitObject) && RaycastGrab(out hitInfo)) // 만약 마우스와 충돌된 오브젝트가 이전에 없었고, 현재 있다면 저장
            {
                // 손 오브젝트 구하기
                wizardLeftHand = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "LeftHandPosition").gameObject;
                wizardRightHand = FindBoneManager_HMJ.Instance.FindBone(GameObject.Find("Player").transform, "RightHandPosition").gameObject;

                hitObject = hitInfo.collider.gameObject;
                effect = effectManager.SpawnAndPlayEffect(hitObject.transform.position, 50000.0f, false, new Vector3(0.0f, 0.0f, 0.0f));



                if (hitObject.name == "Log") // 통나무를 잡았을 경우
                {
                    PlatformDrop_HMJ platformDrop = hitObject.GetComponentInChildren<PlatformDrop_HMJ>();
                    platformDrop.enabled = false;
                }
            }
            else if (hitObject)
            {
                effect.transform.position = hitObject.transform.position;

                wizardRightEffect.SetActive(true);
                wizardRightEffect.transform.position = wizardRightHand.transform.position;

                wizardRightEffect.transform.position = new Vector3(wizardRightEffect.transform.position.x + 1.0f, wizardRightEffect.transform.position.y, wizardRightEffect.transform.position.z);

                wizardLeftEffect.SetActive(true);
                wizardLeftEffect.transform.position = wizardLeftHand.transform.position;

                wizardLeftEffect.transform.position = new Vector3(wizardLeftEffect.transform.position.x + 1.0f, wizardLeftEffect.transform.position.y, wizardLeftEffect.transform.position.z);

                RaycastMove(); // 현재 hit된 보관된 오브젝트를 마우스 위치로 변경(2d -> 3d) (만약 레이케스팅된 물체가 있다면 실행)
                if (Input.GetKey(KeyCode.Z))
                    AddRotationValue(Time.deltaTime * rotateSpeed);
                else if (Input.GetKey(KeyCode.X))
                    AddRotationValue(-Time.deltaTime * rotateSpeed);

            }
        }
        else if(hitObject) // 마우스를 누르지 않을 경우 다시 충돌된 오브젝트를 null로 넣어줌. (해당 정보로 클릭 여부 확인)
        {
            hitObject.GetComponentInChildren<Rigidbody>().useGravity = true;
            hitObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
            hitObject = null;

            if(effect)
                Destroy(effect);

            wizardRightEffect.SetActive(false);
            wizardLeftEffect.SetActive(false);
            //if (wizardRightEffect)
            //    Destroy(wizardRightEffect);

            //if (wizardLeftHand)
            //    Destroy(wizardLeftHand);
            //if (hitObject.name == "Log") // 통나무를 잡았을 경우
            //{
            //    PlatformDrop_HMJ platformDrop = hitObject.GetComponentInChildren<PlatformDrop_HMJ>();
            //    platformDrop.enabled = true;
            //}
        }

    }

    void AddRotationValue(float RotateValue)
    {
        RaycastObjectData_HMJ raycastObjectData = hitObject.GetComponentInChildren<RaycastObjectData_HMJ>();
        if(raycastObjectData)
            raycastObjectData.AddRotateValue(RotateValue);
    }

    void SetRotationValue(float RotateValue)
    {
        RaycastObjectData_HMJ raycastObjectData = hitObject.GetComponentInChildren<RaycastObjectData_HMJ>();
        if (raycastObjectData)
            raycastObjectData.SetRotateValue(RotateValue);
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

}
