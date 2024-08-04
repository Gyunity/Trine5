using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastObjectData_HMJ : MonoBehaviour
{
    float rotateValue = 0.0f;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRotateValue()
    {
        return rotateValue;
    }

    public void AddRotateValue(float _addValue)
    {
        rotateValue += _addValue;
        this.gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, rotateValue);
    }

    public void SetRotateValue(float _rotateValue)
    {
        rotateValue = _rotateValue;
        this.gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, rotateValue);
    }

}
