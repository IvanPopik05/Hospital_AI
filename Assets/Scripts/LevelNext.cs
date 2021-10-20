using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* #refactor
 * 1. Для начала чуть-чуть смущает, что данный скрипт закреплен за объектом Main, а не за LevelPanel, например.
 * Просто по своей логике он отвечает только за текст "Новый уровень". Поэтому лучше его держать там
 * 2. Думаю лучше переменную level перевести в контроллер игры (GameController) или DataController. Иначе получается разброс данных
 * 
 * В общем, пусть LevelNext отвечает только за текст "Новый уровень"  и т.п. и анимацию. А остальное вынести в другое место 
 */
public class LevelNext : MonoBehaviour
{
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private Text showLevelText;
    [SerializeField] private Text meetLevelText;
    [SerializeField] int level = 2;
    [SerializeField] float timer; //#refactor . Название более уточненное лучше. То есть, не совсем ясно, что за таймер.

    ColorScript _mainColor;

    public int Level 
    {
        get => level;
        private set => level = value;
    }
    private void Awake()
    {
        TextOutput();
        showLevelText.text = string.Empty;
        _mainColor = GetComponent<ColorScript>();
    }
    private void TextOutput() 
    {
        meetLevelText.text = $"Соответствует << {level - 1}";
        showLevelText.text = $"{level - 1} УРОВЕНЬ!";
    }
    public void LevelNextMethod() 
    {
        level++;
        TextOutput();
        Debug.Log("Вы перешли на новый уровень!");
    }
    public void GetLevelPanel() 
    {
        StartCoroutine(ShowLevelPanel(0,140));
    }
    public Color GetColorWithText() 
    {
        return showLevelText.color;
    }
    public IEnumerator ShowLevelPanel(float startSize,float endSize) 
    {
        Color startColor = Color.black;
        Color endColor = Color.clear;
        showLevelText.color = _mainColor.GetColor();

        //#refactor Не пойму эту конструкцию)
        //Мы вроде пользуемся Time.deltaTime. Но там же и WaitForSeconds. Переменная t будет получаеть странные значения ведь)
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            levelPanel.GetComponent<Image>().color = Color.Lerp(startColor,endColor,t);
            showLevelText.fontSize = (int)Mathf.Lerp(startSize,endSize,t);
            yield return new WaitForSeconds(timer);
        }
        showLevelText.fontSize = (int)startSize;
        showLevelText.text = string.Empty;
        Debug.Log("Новый Level");
    }
}
