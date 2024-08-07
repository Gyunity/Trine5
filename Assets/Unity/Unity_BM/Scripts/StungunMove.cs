using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StungunMove : MonoBehaviour
{
    // Start is called before the first frame update
    // �̵��ؾ� �ϴ� �Ÿ�
    float movedist;
    // �����ؾ� �ϴ� ���ӿ�����Ʈ
    public GameObject stungunPos;
    public float moveSpeed = 3f;

    // ������ �� �ִ°�?
    public bool canMove;
    void Start()
    {
        // ���� ���� ��ġ���� stungunPos �� ��ġ������ �Ÿ��� ������.
        Vector3 dir = stungunPos.transform.position - transform.position;
        movedist = dir.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // ���࿡ �̵��� �� �� �ִٸ�
        if(canMove)
        {
            // �̵��� ���� ŭ movedist ���� ����
            movedist -= Time.deltaTime;
            // movedis �� 0���� �۰ų� ���� �� ������.
            if(movedist <= 0)
            {
                canMove = false;
            }
            // �Ʒ��� �����̰� �ʹ�.
            transform.position += Vector3.down * moveSpeed *Time.deltaTime;
        }
    }
}
