using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    [SerializeField] Color[] colors;
    private int counter;

    public Color GetColor() 
    {
        return colors[counter++];
    }
    public Color RandomColor() 
    {
        int randColor = Random.Range(0,colors.Length);
        return colors[randColor];
    }
}
