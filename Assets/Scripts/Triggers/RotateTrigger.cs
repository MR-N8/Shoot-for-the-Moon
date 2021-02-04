using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrigger : TriggerSwitch
{

    [SerializeField] Transform transformToRotate;
    [SerializeField] float degrees;

    protected override void OnBallEnter()
    {
        base.OnBallEnter();

        transformToRotate.localEulerAngles = new Vector3(0,0,degrees);
    }
}
