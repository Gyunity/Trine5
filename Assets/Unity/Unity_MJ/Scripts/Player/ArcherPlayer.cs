using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState_HMJ;

public class ArcherPlayer : BasePlayer
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        if (Input.GetMouseButton(0) && playerState.GetState() == PlayerState.Idle)
            playerState.SetState(PlayerState.DrawArrow);
        if (Input.GetMouseButtonUp(0) && playerState.GetState() == PlayerState.DrawArrow)
            playerState.SetState(PlayerState.ShootArrow);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("충돌" + collision.gameObject.name);
    }
}
