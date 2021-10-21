using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpPuddle : GAction
{
    public override bool PostPreform()
    {
        inventory.RemoveItem(target);
        Destroy(target);
        return true;
    }

    public override bool PrePreform()
    {
        target = GWorld.Instance.GetQueue("puddles").RemoveResource();
        if (target == null)
            return false;
        GWorld.Instance.GetWorld().ModifyState("FreePuddle", -1);
        inventory.AddItem(target);

        return true;
    }
}
