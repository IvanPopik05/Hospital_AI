using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHospital : GAction
{
    public override bool PostPreform()
    {
        return true;
    }

    public override bool PrePreform()
    {
        return true;
    }
}
