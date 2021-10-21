using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : GAction
{
    public override bool PostPreform()
    {
        return true;
    }

    public override bool PrePreform()
    {
        beliefs.RemoveState("exhausted");
        return true;
    }
}
