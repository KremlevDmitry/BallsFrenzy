using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private const float SPEED = 5f;

    private Image image = null;
    private Image _image => image ??= GetComponent<Image>();

    public int X = default;
    public int Y = default;
    public Sprite Sprite
    {
        get => _image.sprite;
        set => _image.sprite = value;
    }
    private Game _game = default;

    public static float Height = default;
    public Vector3? StartPosition = null;
    private static int _animationCount = 0;


    public void Init(int x, int y, Sprite sprite, Game game)
    {
        _game = game;
        X = x;
        Y = y;
        Sprite = sprite;
    }

    public void Select()
    {
        transform.localScale = new Vector3(.9f, .9f, 1f);
    }
    public void UnSelect()
    {
        transform.localScale = Vector3.one;
    }

    public void MoveToStart(bool isAnimated)
    {
        if (StartPosition == null)
        {
            StartPosition = transform.position;
        }

        if (isAnimated)
        {
            StartCoroutine(MovingToStart());
        }
        else
        {
            transform.position = StartPosition.Value;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_animationCount > 0) { return; }

        _game?.AddToList(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_animationCount > 0) { return; }

        _game?.FinishList();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_animationCount > 0) { return; }

        _game?.StartList();
        _game?.AddToList(this);
    }

    private IEnumerator MovingToStart()
    {
        _animationCount++;
        while (transform.position != StartPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPosition.Value, SPEED * Time.deltaTime);
            yield return null;
        }
        yield return null;
        _animationCount--;
    }
}
