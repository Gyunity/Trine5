using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState_HMJ;

public class WarrorPlayer : BasePlayer
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0) && (playerState.GetState() != PlayerState.Attack00))
            playerState.SetState(PlayerState.Attack00);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("충돌" + collision.gameObject.name);
    }
}
