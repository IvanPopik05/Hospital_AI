using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoom : GAction
{
    public override bool PostPreform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
        GWorld.Instance.GetQueue("patients").AddResource(gameObject);
        beliefs.ModifyState("atHospital",1);
        return true;
    }

    public override bool PrePreform()
    {
        return true;
    }
}
