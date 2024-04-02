using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Insect
{
    protected override IEnumerator Movement()
    {
        return base.Movement();
    }

    protected override void MoveToTarget(Vector3 target, float duration)
    {
        AnimSetFloat("speed", 1);
        AnimSetBool("fly", true);
        base.MoveToTarget(target, duration);
    }
}
