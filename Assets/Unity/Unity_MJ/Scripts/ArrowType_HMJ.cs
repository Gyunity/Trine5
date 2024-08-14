using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrowType_HMJ;

public class ArrowType_HMJ : MonoBehaviour
{
    public enum ArrowType
    {
        ArrowFireType,
        ArrowIceType,
        ArrowTypeEnd
    }

    ArrowType curArrowType;
    float AttackValue = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        curArrowType = ArrowType.ArrowFireType;
    }

    // Update is called once per frame
    void Update()
    {
        InputChangeArrowType();
        Debug.Log("현재 화살 타입: " + curArrowType);
    }

    void InputChangeArrowType()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (GetArrowType() == ArrowType.ArrowIceType)
                SetArrowType(ArrowType.ArrowFireType);
            else
                SetArrowType(ArrowType.ArrowIceType);
        }
    }

    public void SetArrowType(ArrowType arrowType)
    {
        curArrowType = arrowType;

        switch(arrowType)
        {
            case ArrowType.ArrowIceType:
                AttackValue = 100.0f;
                break;
            case ArrowType.ArrowFireType:
                AttackValue = 100.0f;
                break;
        }
    }

    public ArrowType GetArrowType()
    {
        return curArrowType;
    }

}
