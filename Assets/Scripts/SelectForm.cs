using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectForm : MonoBehaviour
{
    [SerializeField] private MessagePanel messagePanel;
    [SerializeField] private SpriteRenderer main_sprite;
    [Header("Data")]
    public DataFigure[] dataFigures;
    private DataFigure spriteData;

    private bool isActive = false;
    private Animator main_sprite_anim;

    LevelNext _levelNext;
    ColorScript _mainColor;
    private int counter = 0;

    private const int maxLevel = 7;
    private const int levelStepsBack = 2; 

    private void Awake()
    {
        main_sprite_anim = main_sprite.GetComponent<Animator>();
        _levelNext = GetComponent<LevelNext>();
        _mainColor = GetComponent<ColorScript>();
    }
    private void Start()
    {
        Initialize();
    }

    private void RandomTypeFigure() 
    {
        messagePanel.TypeFigure = (TaskType)Random.Range(0,2);
    }
    private void SetDataFigure() 
    {
        spriteData = dataFigures[_levelNext.Level - levelStepsBack];
    }
    private void SetNewSprite(SpriteRenderer sprite) 
    {
        sprite.sprite = RandomSprites(GetSprites());
        sprite.color = _mainColor.RandomColor();
        ArrayOfShapes.AddElement(main_sprite);
    }
    private void Initialize() 
    {
        ArrayOfShapes.AddElement(main_sprite);
        SetDataFigure();
        ChangeCard();
    }

    private void ChangeCard()
        => StartCoroutine(ChangeCardCoroutine());
    private IEnumerator ChangeCardCoroutine()
    {
        CardAnim(true, false);

        yield return new WaitForSeconds(2);

        SetNewSprite(main_sprite);

        CardAnim(false, true);
    }
    private void CardAnim(bool isHide, bool isShow)
    {
        main_sprite_anim.SetBool("Hide", isHide);
        main_sprite_anim.SetBool("Show", isShow);
    }
    private void Won() 
    {
        Debug.Log("Вы угадали");
        RandomTypeFigure();
        messagePanel.SelectMessage();
        counter++;
        if(ArrayOfShapes.AllSprites.Count > maxLevel || ArrayOfShapes.AllColors.Count > maxLevel)
            ArrayOfShapes.RemoveElement();
        if (counter >= maxLevel) 
        {
            StartCoroutine(_levelNext.ShowLevelPanel(0,140));
            _levelNext.LevelNextMethod();
            counter = 0;
            SetDataFigure();
        }
        ChangeCard();
    }

    private bool TypeFormOrColor() 
    {
        switch (messagePanel.TypeFigure)
        {
            case TaskType.Form:
                return ArrayOfShapes.IsSprite(_levelNext.Level,main_sprite);
            case TaskType.Color:
                return ArrayOfShapes.IsColor(_levelNext.Level,main_sprite);
        }
        return true;
    }
    public void SelectBtn(bool isEven) 
    {
        isActive = !isActive;
        if ((TypeFormOrColor() && isEven) || (!TypeFormOrColor() && !isEven))
        {
            Won();
        }
        else 
        {
            Debug.Log("Вы не угадали");
            Repeat();
        }
    }
    private void Repeat() 
    {
        Debug.Log("Повторите");
    }

    private Sprite[] GetSprites() 
    {
        return spriteData.sprites;
    }
    private Sprite RandomSprites(Sprite[] sprites) 
    {
        int randSprite = Random.Range(0,sprites.Length);
        return sprites[randSprite];
    }

}
