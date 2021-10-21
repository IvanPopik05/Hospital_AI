using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : GAction
{
    public override bool PostPreform()
    {
        Destroy(gameObject);
        return true;
    }

    public override bool PrePreform()
    {
        beliefs.RemoveState("atHospital");
        return true;
    }
}
