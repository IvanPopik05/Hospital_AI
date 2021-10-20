using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Этот класс отвечает за хранение данных цвета и спрайта
public class ArrayOfShapes
{
    public static List<Sprite> AllSprites { get; private set; } = new List<Sprite>();
    public static List<Color> AllColors { get; private set; } = new List<Color>();

    /* #refactor
     * Методы AddElements и RemoveElements
     * Название говорит о том, что добавится несколько элементов. Но по факту заносится только одно и удаляется только одно
     */
    public static void AddElement(SpriteRenderer main_sprite)
    {
        AllSprites.Add(main_sprite.sprite);
        AllColors.Add(main_sprite.color);
    }
    public static void RemoveElement()
    {
        AllSprites.RemoveAt(0);
        AllColors.RemoveAt(0);
    }
    public static bool IsSprite(int level, SpriteRenderer main_sprite)
    {
        return AllSprites[AllSprites.Count - level] == main_sprite.sprite;
    }
    public static bool IsColor(int level, SpriteRenderer main_sprite)
    {
        return AllColors[AllColors.Count - level] == main_sprite.color;
    }
}
