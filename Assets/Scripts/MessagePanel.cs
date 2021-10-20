using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    private Text[] messages;

    public TaskType TypeFigure { get; set; } = TaskType.Form;
    private void Start()
    {
        messages = GetComponentsInChildren<Text>();
        SelectMessage();
    }
    private void ActiveMessage(bool messageForm,bool messageColor) 
    {
        messages[0].enabled = messageColor;
        messages[1].enabled = messageForm;
    }
    public void SelectMessage() 
    {
        switch (TypeFigure)
        {
            case TaskType.Form:
                ActiveMessage(true,false);
                break;
            case TaskType.Color:
                ActiveMessage(false, true);
                break;
            default:
                break;
        }
    }
}
