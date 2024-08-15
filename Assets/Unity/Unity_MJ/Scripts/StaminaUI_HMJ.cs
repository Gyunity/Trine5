using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class StaminaUI_HMJ : MonoBehaviour
{
    Image imageData;
    public Material targetMaterial;
    // Start is called before the first frame update
    void Start()
    {
        imageData = GetComponent<Image>();

        targetMaterial = imageData.material;
    }

    // Update is called once per frame

}
