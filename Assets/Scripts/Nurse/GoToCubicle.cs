using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
{
    public override bool PostPreform()
    {
        GWorld.Instance.GetWorld().ModifyState("TreatingPatient", 1);
        GWorld.Instance.GetQueue("cubicles").AddResource(target); // Добавляет кабинку
        inventory.RemoveItem(target); // Удаляет из инвентаря
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle",1); // Делает кабинку свободной
        return true;
    }

    public override bool PrePreform()
    {
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null)
            return false;
        return true;
    }
}
