using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

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
    ParticleSystem Flash;

    public Material[] laserMat;

    bool onFlash = true;
    bool onFlash2 = true;

    int playerlay = 0;

    public float shellGageup = 0.3f;

    void Start()
    {
        valeribot = GameObject.Find("Valeribot_GH");
        valeribotSC = valeribot.GetComponent<ValeribotFSM_GH>();
        laserLine = GetComponent<LineRenderer>();
        effects = GetComponentsInChildren<ParticleSystem>();
        hit = hitEffect.GetComponentsInChildren<ParticleSystem>();
        Flash = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        playerlay = valeribotSC.onReadyLaser ? 0 : 1;


        //라인렌더 위치
        if (laserLine != null && updateSaver == false)
        {

            laserLine.SetPosition(0, transform.position);
            RaycastHit rayHit;
            //플레이어와 그라운드만 레이를 쏜다.
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, maxLength, playerlay << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Thorn") | 1 << LayerMask.NameToLayer("SummonedObject")))
            {
                //끝 레이저 위치가 오브젝트이면
                laserLine.SetPosition(1, rayHit.point);


                if (!valeribotSC.onReadyLaser)
                {
                    playerlay = 1;
                    if (onFlash)
                    {
                        Flash.Play();
                        onFlash = false;

                    }

                    laserLine.textureScale = new Vector2(1, 1);
                    laserLine.material = laserMat[0];
                    hitEffect.transform.position = rayHit.point + rayHit.normal * hitOffset;
                    if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Ground") && valeribotSC.onRtateLaser == false)
                    {
                        GameObject thorn = Instantiate(thornFactory);
                        thorn.transform.position = rayHit.point + rayHit.normal * hitOffset;
                        thorn.transform.up = rayHit.normal;
                        Destroy(thorn, 5);
                    }

                    if (rayHit.transform.gameObject.tag == "Cabbage")
                    {
                        Image shell2Gage = rayHit.transform.gameObject.GetComponentInChildren<Image>();
                        if (shell2Gage != null)
                        {

                            shell2Gage.fillAmount += shellGageup * Time.deltaTime;
                        }
                    }
                }
                else
                {
                    onFlash = true;
                    Flash.Stop();
                    laserLine.textureScale = new Vector2(2, 1.5f);
                    laserLine.material = laserMat[1];
                    hitEffect.transform.position = Vector3.down * 200;

                }

                //foreach (var allPs in effects)
                //{
                //    if (allPs.isPlaying)
                //    {
                //        if (!valeribotSC.onReadyLaser)
                //        {
                //            //여기서 플래쉬 빼고 시작하게
                //            //allPs.Play();
                //        }
                //    }
                //}
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

                        if (!valeribotSC.onReadyLaser)
                        {
                            allPs.Play();

                        }
                    }
                }
                length[0] = mainTextureLength * (Vector3.Distance(transform.position, endPos));
                length[2] = noiseTextureLength * (Vector3.Distance(transform.position, endPos));

                if (!valeribotSC.onReadyLaser)
                {
                    if (onFlash2)
                    {
                        Flash.Play();
                        onFlash2 = false;
                    }
                    laserLine.material = laserMat[0];
                    laserLine.textureScale = new Vector2(1, 1);

                }

                else
                {
                    Flash.Stop();
                    onFlash2 = true;
                    laserLine.material = laserMat[1];
                    laserLine.textureScale = new Vector2(2, 1.5f);

                }

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

    public void LaserDone()
    {
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

        if (hit != null)
        {
            foreach (var allPs in hit)
            {
                if (allPs.isPlaying)
                {
                    allPs.Stop();

                }
            }

        }
        laserLine.enabled = false;
        laserSaver = false;
        gameObject.SetActive(false);
    }

}
