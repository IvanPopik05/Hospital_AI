using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    [SerializeField] Text states;
    void LateUpdate()
    {
        var worldStates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach (var w in worldStates)
        {
            states.text += $"{w.Key} {w.Value}\n";
        }
    }
}
