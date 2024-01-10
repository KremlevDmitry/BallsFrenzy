using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField]
    private GameObject _game = default;
    [SerializeField]
    private Button _restartButton = default;
    [SerializeField]
    private Button _nextLevelButton = default;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() =>
        {
            _game.SetActive(false);
            _game.SetActive(true);
        });
        _nextLevelButton.onClick.AddListener(() =>
        {
            Levels.Current++;
            _game.SetActive(false);
            _game.SetActive(true);
        });
    }
}
