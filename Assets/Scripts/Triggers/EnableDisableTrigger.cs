using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableTrigger : TriggerSwitch
{

    [SerializeField] GameObject objectToAffect;
    [SerializeField] bool doEnable;

    protected override void OnBallEnter()
    {
        base.OnBallEnter();

        objectToAffect.SetActive(doEnable);
    }
}
