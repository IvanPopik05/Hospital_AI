using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounting : MonoBehaviour
{
    [SerializeField] private float timer;
    private Text _countingTime;
    private void Awake()
    {
        _countingTime = GetComponent<Text>();
        _countingTime.text = "0:00";
    }
    private void Update()
    {
        if (timer >= 0)
        {
            _countingTime.text = $"{(int)timer}";
            timer -= Time.deltaTime;
        }
        else 
        {
            Debug.Log("Вы проиграли, начните сначала");
        }
    }
}
