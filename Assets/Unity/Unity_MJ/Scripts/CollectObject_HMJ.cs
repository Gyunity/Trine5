using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject_HMJ : MonoBehaviour
{
    public GameObject sceneSYS;
    SceneStory_GH storySC;
    // Start is called before the first frame update
    void Start()
    {
        storySC = sceneSYS.GetComponent<SceneStory_GH>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 콜리전 충돌하면 삭제
        if (other.name.Contains("Player"))
        {
            storySC.GemCount++;
            SoundManager.instance.PlayStoryEftSound(SoundManager.EStoryEftType.STORY_ITEMGET);

            Destroy(gameObject);
        }
    }
}
