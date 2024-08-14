using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static EnemyMove;


public class StunTrigger : MonoBehaviour
{
    private float currTime;
    private float creatTime = 2f;

    //public GameObject stt;
    //private bool isOn = true;

    GameObject target;
    EnemyMove boss;

    private void Start()
    {
        target = GameObject.Find("Boss_BM");
        boss =target.GetComponent<EnemyMove>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player_equipment") 
        {
            currTime += Time.deltaTime;
            if (currTime >= creatTime)
            {
                Debug.Log("공격가능!!!!!");
                //스턴 애니메이션
                boss.ChangeState(EEnemyState.Stun);
                currTime = 0;
                //hitsystem On
                gameObject.SetActive(false);
                
            }
            
        }
        
    }
    
}
