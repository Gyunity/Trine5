using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_GH : MonoBehaviour
{
    //대포쏘기
    // 포탄 프리팹
    public GameObject shellFactory;
    // 대포의 발사 위치
    public Transform firePoint;
    // 발사 힘
    public float launchForce = 10f;
    // 발사 각도
    public float angle = 45f;

    //포탄 장전
    public bool shellLoad;
    public bool arrowFire;

    // 폭탄 로프
    public GameObject rope;

    public GameObject fire;
    void Start()
    {
        shellLoad = false;
        //ToDO 화살 되면 바꾸기 false로
        arrowFire = false;

        if (rope != null)
        {
            rope.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        //장전이 되었으면
        if (shellLoad == true && arrowFire == true)
        {
            Invoke("shoot", 2);
            shellLoad = false;

            arrowFire = false;
        }

        if (shellLoad)
        {
            if (rope != null)
                rope.SetActive(true);
        }
        else
        {
            if (rope != null)
                rope.SetActive(false);

        }
    }

    void shoot()
    {
        SoundManager.instance.PlayBossEftSound(SoundManager.EBossEftType.BOSS_CANNON);

        //포탄 생성
        GameObject shell = Instantiate(shellFactory, firePoint.position, firePoint.rotation);

        //포탄의 Rigidbody
        Rigidbody rb = shell.GetComponent<Rigidbody>();

        //발사각도 라디안으로 변경
        float radianAngle = angle * Mathf.Deg2Rad;

        //발사 백터 계산
        Vector3 launchDirection = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);

        //발사 힘 적용
        rb.velocity = launchDirection * launchForce;
    }
    private void OnTriggerEnter(Collider other)
    {
        ArrowMove_HMJ arrowType = other.GetComponent<ArrowMove_HMJ>();

        if (other.gameObject.layer == LayerMask.NameToLayer("Arrow") && arrowType.arrowType == ArrowType_HMJ.ArrowType.ArrowIceType)
        {
            fire.SetActive(false);
        }
    }
}
