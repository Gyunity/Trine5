using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayer : BasePlayer
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

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("충돌" + collision.gameObject.name);
    }
}
