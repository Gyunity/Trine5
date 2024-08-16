using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager_HMJ : MonoBehaviour
{
    public GameObject effectPrefab;  // 이펙트 프리팹을 에디터에서 할당

    public GameObject effectPrefab2;  // 이펙트 프리팹을 에디터에서 할당
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnAndPlayEffect(Vector3 position, float duration, bool bfoward, Vector3 foward)
    {
        // 이펙트를 특정 위치에 인스턴스화
        GameObject effectInstance = Instantiate(effectPrefab, position, Quaternion.identity);

        if(bfoward)
            effectInstance.transform.rotation = Quaternion.LookRotation(foward);
        // 파티클 시스템 컴포넌트를 가져옵니다.
        ParticleSystem particleSystem = effectInstance.GetComponent<ParticleSystem>();

        // 파티클 시스템이 존재한다면 재생합니다.
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        Destroy(effectInstance, duration);

        return effectInstance;
    }

    public GameObject SpawnAndPlayEffect2(Vector3 position, float duration, bool bfoward, Vector3 foward)
    {
        // 이펙트를 특정 위치에 인스턴스화
        GameObject effectInstance = Instantiate(effectPrefab2, position, Quaternion.identity);

        if (bfoward)
            effectInstance.transform.rotation = Quaternion.LookRotation(foward);
        // 파티클 시스템 컴포넌트를 가져옵니다.
        ParticleSystem particleSystem = effectInstance.GetComponent<ParticleSystem>();

        // 파티클 시스템이 존재한다면 재생합니다.
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        Destroy(effectInstance, duration);

        return effectInstance;
    }

    public GameObject SpawnAndPlayEffect3(Vector3 position, float duration, bool bfoward, Vector3 foward)
    {
        // 이펙트를 특정 위치에 인스턴스화
        GameObject effectInstance = Instantiate(effectPrefab2, position, Quaternion.identity);

        if (bfoward)
            effectInstance.transform.rotation = Quaternion.LookRotation(foward);
        // 파티클 시스템 컴포넌트를 가져옵니다.
        ParticleSystem particleSystem = effectInstance.GetComponent<ParticleSystem>();

        // 파티클 시스템이 존재한다면 재생합니다.
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        return effectInstance;
    }
    //// 일정 시간 후에 이펙트를 제거

}
