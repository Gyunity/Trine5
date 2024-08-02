using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire_GH : MonoBehaviour
{
    GameObject valeribot;
    ValeribotFSM_GH valeribotSC;

    public GameObject thornFactory;

    public GameObject hitEffect;
    public float hitOffset = 0;
    public bool useLaserRotation = false;

    public float maxLength;
    private LineRenderer laserLine;

    public float mainTextureLength = 1f;
    public float noiseTextureLength = 1f;
    private Vector4 length = new Vector4(1, 1, 1, 1);

    private bool laserSaver = false;
    private bool updateSaver = false;

    private ParticleSystem[] effects;
    private ParticleSystem[] hit;

    public Material[] laserMat;

    void Start()
    {
        valeribot = GameObject.Find("Valeribot_GH");
        valeribotSC = valeribot.GetComponent<ValeribotFSM_GH>();
        laserLine = GetComponent<LineRenderer>();
        effects = GetComponentsInChildren<ParticleSystem>();
        hit = hitEffect.GetComponentsInChildren<ParticleSystem>();

    }

    void Update()
    {
        //라인렌더 위치
        if (laserLine != null && updateSaver == false)
        {
            laserLine.SetPosition(0, transform.position);
            RaycastHit rayHit;
            //플레이어와 그라운드만 레이를 쏜다.
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, maxLength, 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Thorn")))
            {
                //끝 레이저 위치가 오브젝트이면
                laserLine.SetPosition(1, rayHit.point);


                if (!valeribotSC.onReadyLaser)
                {
                        hitEffect.transform.position = rayHit.point + rayHit.normal * hitOffset;
                        laserLine.material = laserMat[0];
                    if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        GameObject thorn = Instantiate(thornFactory);
                        thorn.transform.position = rayHit.point + rayHit.normal * hitOffset;
                        Destroy(thorn, 5);
                    }
                }
                else
                {
                    hitEffect.transform.position = Vector3.up * 200;
                    laserLine.material = laserMat[1];

                }
                if (useLaserRotation)
                {
                    hitEffect.transform.rotation = transform.rotation;
                }
                else
                {
                    hitEffect.transform.LookAt(rayHit.point + rayHit.normal);
                }

                foreach (var allPs in effects)
                {
                    if (allPs.isPlaying)
                    {
                        allPs.Play();
                    }
                }
                //텍스처 틸팅
                length[0] = mainTextureLength * (Vector3.Distance(transform.position, rayHit.point));
                length[2] = noiseTextureLength * (Vector3.Distance(transform.position, rayHit.point));

            }
            else
            {
                //레이저의 끝에 아무것도 없으면
                var endPos = transform.position + transform.forward * maxLength;
                laserLine.SetPosition(1, endPos);
                hitEffect.transform.position = endPos;
                foreach (var allPs in hit)
                {
                    if (allPs.isPlaying)
                    {
                        allPs.Play();
                    }
                }
                length[0] = mainTextureLength * (Vector3.Distance(transform.position, endPos));
                length[2] = noiseTextureLength * (Vector3.Distance(transform.position, endPos));

                if (!valeribotSC.onReadyLaser)
                    laserLine.material = laserMat[0];
                else
                    laserLine.material = laserMat[1];

            }


            //텍스처 크기
            laserLine.material.SetTextureScale("_MainTex", new Vector2(length[0], length[1]));
            laserLine.material.SetTextureScale("_Noise", new Vector2(length[2], length[3]));



            if (laserLine.enabled == false && laserSaver == false)
            {
                laserSaver = true;
                laserLine.enabled = true;
            }


        }

    }
    public void DIsablePrepare()
    {
        if (laserLine != null)
        {
            laserLine.enabled = false;
        }
        updateSaver = true;

        if (effects != null)
        {
            foreach (var allPs in effects)
            {
                if (allPs.isPlaying)
                {
                    allPs.Stop();
                }
            }
        }
    }

    public void LaserDone()
    {
        laserLine.enabled = false;
        laserSaver = false;
        gameObject.SetActive(false);
        //laserLine.SetPosition(0, Vector3.zero);
        //laserLine.SetPosition(1, Vector3.zero);
    }

}
