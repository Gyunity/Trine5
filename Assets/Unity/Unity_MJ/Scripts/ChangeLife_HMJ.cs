using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLife_HMJ : MonoBehaviour
{
    public Image uiImage;
    public Sprite[] sprite;

    public int lifeN = 3;
    HPSystem_HMJ hpSystem;
    ChangeCharacter changeCharacter;
    // Start is called before the first frame update
    void Start()
    {
        lifeN = 3;

        hpSystem = GameObject.Find("Player").GetComponentInChildren<HPSystem_HMJ>();
        changeCharacter = GameObject.Find("Player").GetComponentInChildren<ChangeCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeN = hpSystem.lifeData[(int)changeCharacter.GetPlayerCharacterType()];
        if(lifeN > 0)
            uiImage.sprite = sprite[lifeN - 1];
    }
}
