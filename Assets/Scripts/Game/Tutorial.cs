using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private Button _button = default;
    [SerializeField]
    private Image _image = default;
    private int _current = default;
    [SerializeField]
    private Sprite[] _sprites = default;
    [SerializeField]
    private GameObject _game = default;


    private void Awake()
    {
        _button.onClick.AddListener(() => { Open(_current + 1); });
    }

    private void OnEnable()
    {
        Open(0);
    }


    private void Open(int id)
    {
        _current = id;

        if (_current < _sprites.Length)
        {
            _image.sprite = _sprites[_current];
        }
        else
        {
            gameObject.SetActive(false);
            _game.SetActive(true);
        }
    }
}
