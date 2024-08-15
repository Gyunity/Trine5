using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorUI_HMJ : MonoBehaviour
{
    HPSystem_HMJ playerHpSystem;

    public Material targetMaterial;
    public Vector3 firstColorData;

    public Vector3 lastColorData;
    Image imageData;

    float delayTime = 2.0f;
    float maxLerpTime = 1.0f;
    float lerpTime = 0.0f;

    float hpValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        imageData = GetComponent<Image>();

        targetMaterial = imageData.material;

        // 153, 243, 111

        firstColorData = new Vector3(153.0f / 255.0f, 243.0f / 255.0f, 111.0f / 255.0f);
        lastColorData = new Vector3(255.0f / 255.0f, 0.0f, 0.0f);

        playerHpSystem = GameObject.Find("Player").GetComponentInChildren<HPSystem_HMJ>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = playerHpSystem.currHP[(int)GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>().GetPlayerCharacterType()] / playerHpSystem.maxHP;
        Vector3 lerpValue = Vector3.Lerp(firstColorData, lastColorData, 1.0f - value);
        imageData.color = new Color(lerpValue.x, lerpValue.y, lerpValue.z, 1.0f);
        //targetMaterial.tint = new Vector4(lerpValue.x, lerpValue.y, lerpValue.z, 1.0f);
    }
}
