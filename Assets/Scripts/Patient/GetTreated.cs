using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{
    public override bool PostPreform()
    {
        GWorld.Instance.GetWorld().ModifyState("Treated",1);
        beliefs.ModifyState("isCured",1);
        inventory.RemoveItem(target); // Удаляет кабинку
        return true;
    }

    public override bool PrePreform()
    {
        target = inventory.FindItemWithTag("Cubicle"); // Ищет кабинку
        if (target == null)
            return false;
        return true;
    }
}
