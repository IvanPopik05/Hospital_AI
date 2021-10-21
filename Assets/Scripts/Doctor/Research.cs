using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research : GAction
{
    public override bool PostPreform()
    {
        GWorld.Instance.GetQueue("offices").AddResource(target);
        GWorld.Instance.GetWorld().ModifyState("FreeOffice",1);
        inventory.RemoveItem(target);
        return true;
    }

    public override bool PrePreform()
    {
        target = GWorld.Instance.GetQueue("offices").RemoveResource();
        if (target == null)
            return false;
        GWorld.Instance.GetWorld().ModifyState("FreeOffice",-1);
        inventory.AddItem(target);
        return true;
    }
}
