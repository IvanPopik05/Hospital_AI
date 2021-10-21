using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToToilet : GAction
{
    public override bool PostPreform()
    {
        GWorld.Instance.GetQueue("toilets").AddResource(target);
        GWorld.Instance.GetWorld().ModifyState("FreeToilet",1);
        inventory.RemoveItem(target);
        beliefs.RemoveState("busting");
        return true;
    }

    public override bool PrePreform()
    {
        target = GWorld.Instance.GetQueue("toilets").RemoveResource();
        if (target == null)
            return false;
        GWorld.Instance.GetWorld().ModifyState("FreeToilet", -1);
        inventory.AddItem(target);
        return true;
    }
}
