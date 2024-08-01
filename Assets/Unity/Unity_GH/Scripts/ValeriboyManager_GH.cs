using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValeriboyManager_GH : MonoBehaviour
{
    public static ValeriboyManager_GH instance;

    public GameObject shield;
    public bool onShield = true;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        OnShield();
    }
    void OnShield()
    {
        if (onShield)
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
    }

}
