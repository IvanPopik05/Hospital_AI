using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "resourcedata", menuName = "Resource Data",order = 51)]
public class ResourceData : ScriptableObject
{
    public string resourceTag; // Tag
    public string resourceQueue; // Название очереди ресурса
    public string resourceState; // Состояние ресурса
}
