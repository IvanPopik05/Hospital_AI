using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : GAgent
{
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("research",1,false);
        goals.Add(s1,1);
        SubGoal s2 = new SubGoal("relief", 1, false);
        goals.Add(s2, 2);
        SubGoal s3 = new SubGoal("rested",1,false);
        goals.Add(s3,3);
        Invoke("GetTired", Random.Range(5, 15));
        Invoke("NeedRelief", Random.Range(7,10));
    }
    void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        Invoke("GetTired", Random.Range(5, 15));
    }
    void NeedRelief()
    {
        beliefs.ModifyState("busting", 0);
        Invoke("NeedRelief", Random.Range(7, 10));
    }
}
