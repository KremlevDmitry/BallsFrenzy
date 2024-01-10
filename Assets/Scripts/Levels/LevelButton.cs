using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private int _id = default;
    [SerializeField]
    private Button _button = default;
    [SerializeField]
    private Image _image = default;
    [SerializeField]
    private Text _text = default;
    [SerializeField]
    private GameObject _starsObject = default;
    [SerializeField]
    private Image[] _stars = default;

    [SerializeField]
    private Sprite _closeStarSprite = default;
    [SerializeField]
    private Sprite _openStarSprite = default;
    [SerializeField]
    private Sprite _closeLevelSprite = default;
    [SerializeField]
    private Sprite _openLevelSprite = default;

    [SerializeField]
    private GameObject _levelsPage = default;
    [SerializeField]
    private GameObject _gamePage = default;


    private void Awake()
    {
        _button.onClick.AddListener(OpenGame);
    }

    public void Set(int id)
    {
        _id = id;

        bool isEnable = Levels.Max >= id;
        bool isCurrent = Levels.Max == id;

        _button.interactable = isEnable;
        _image.sprite = isEnable ? _openLevelSprite : _closeLevelSprite;
        _text.text = $"{id + 1}";
        _text.enabled = isEnable;
        _starsObject.SetActive(isEnable);

        foreach (var star in _stars)
        {
            star.sprite = isCurrent ? _closeStarSprite : _openStarSprite;
        }
    }

    private void OpenGame()
    {
        Levels.Current = _id;
        _levelsPage.SetActive(false);
        _gamePage.SetActive(true);
    }
}
