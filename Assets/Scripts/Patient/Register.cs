using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : GAction
{
    public override bool PostPreform()
    {
        beliefs.ModifyState("atHospital",0);
        return true;
    }

    public override bool PrePreform()
    {
        return true;
    }
}
