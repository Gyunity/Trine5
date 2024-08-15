using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager_HMJ : MonoBehaviour
{
    public GameObject effectPrefab;  // 이펙트 프리팹을 에디터에서 할당
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAndPlayEffect(Vector3 position, float duration)
    {
        // 이펙트를 특정 위치에 인스턴스화
        GameObject effectInstance = Instantiate(effectPrefab, position, Quaternion.identity);

        // 파티클 시스템 컴포넌트를 가져옵니다.
        ParticleSystem particleSystem = effectInstance.GetComponent<ParticleSystem>();

        // 파티클 시스템이 존재한다면 재생합니다.
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        // 일정 시간 후에 이펙트를 제거
        Destroy(effectInstance, duration);
    }
}
