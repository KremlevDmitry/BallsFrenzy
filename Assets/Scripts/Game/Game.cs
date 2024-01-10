using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject _tutorial = default;

    [SerializeField]
    private Text _scoreText = default;
    [SerializeField]
    private Text[] _goalTexts = default;
    private int _score = default;
    private int[] _goals = default;


    [SerializeField]
    private Ball[] _balls = default;
    private List<Ball> _list = null;
    [SerializeField]
    private Sprite[] _enableBallSprite = default;

    [SerializeField]
    private GameObject _win = default;


    private void Awake()
    {
        Ball.Height = _balls[0].transform.position.y - _balls[5].transform.position.y;
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("FirstGame", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstGame", 1);
            gameObject.SetActive(false);
            _tutorial.SetActive(true);
        }
        else
        {
            StartGame();
        }
    }


    public void StartList()
    {
        _list = new List<Ball>();
    }
    public void AddToList(Ball ball)
    {
        if (_list == null) { return; }

        if (_list.Contains(ball)) { return; }

        if (_list.Count > 0)
        {
            var last = _list[_list.Count - 1];
            if (last.Sprite != ball.Sprite) { return; }
            if (!(((last.X == ball.X) && (Mathf.Abs(last.Y - ball.Y) == 1)) ||
                ((last.Y == ball.Y) && (Mathf.Abs(last.X - ball.X) == 1)))) { return; }
        }
        ball.Select();
        _list.Add(ball);
    }
    public void FinishList()
    {
        if (_list == null) { return; }

        foreach (var ball in _list)
        {
            ball.UnSelect();
        }

        if (_list.Count < 3)
        {
            _list = null;
            return;
        }

        UpdateScore(_score + _list.Count);
        UpdateGoals(_list[0].Sprite, _list.Count);
        foreach (var ball in _list)
        {
            ball.Sprite = null;
        }

        for (int i = 0; i < 5; i++)
        {
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (_balls[j * 5 + i].Sprite == null)
                    {
                        count++;
                    }
                }
                for (int j = 0; j < count; j++)
                {
                    _balls[j * 5 + i].transform.position += Ball.Height * count * Vector3.up;
                }
                int qwe = 0;
                for (int j = count; j < 5; j++)
                {
                    for (int k = qwe; k < 5; k++)
                    {
                        if (_balls[k * 5 + i].Sprite != null)
                        {
                            qwe = k + 1;
                            _balls[j * 5 + i].transform.position = _balls[k * 5 + i].StartPosition.Value;
                            break;
                        }
                    }
                }
            }
            for (int j = 4; j >= 0; j--)
            {
                var current = _balls[j * 5 + i];
                if (current.Sprite == null)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (_balls[k * 5 + i].Sprite != null)
                        {
                            (_balls[k * 5 + i].Sprite, current.Sprite) = (current.Sprite, _balls[k * 5 + i].Sprite);
                            break;
                        }
                    }
                }
            }
        }

        foreach (var ball in _balls)
        {
            if (ball.Sprite == null)
            {
                ball.Sprite = GetRandomSprite();
            }
            ball.MoveToStart(true);
        }

        _list = null;
    }


    private void StartGame()
    {
        UpdateScore(0);
        CreateGoals();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                _balls[i * 5 + j].Init(j, i, GetRandomSprite(), this);
                _balls[i * 5 + j].MoveToStart(false);
            }
        }
    }

    private void UpdateScore(int value)
    {
        _score = value;
        _scoreText.text = $"Score: {value}";
    }

    private void CreateGoals()
    {
        var sum = (Levels.Current + 1) * 20;
        _goals = new[] { 0, 0, 0 };
        for (int i = 0; i < _goals.Length; i++)
        {
            _goals[i] = Random.Range(1, sum);
            sum -= _goals[i];
        }
        int index = 0;
        int min = int.MaxValue;
        for (int i = 0; i < _goals.Length; i++)
        {
            if (min > _goals[i])
            {
                index = i;
                min = _goals[i];
            }
        }
        _goals[index] += sum;

        for (int i = 0; i < _goals.Length; i++)
        {
            _goalTexts[i].text = $"x{_goals[i]}";
        }
    }

    private void UpdateGoals(Sprite sprite, int value)
    {
        for (int i = 0; i < _enableBallSprite.Length; i++)
        {
            if (_enableBallSprite[i] == sprite)
            {
                UpdateGoal(i, value);
            }
        }
    }

    private void UpdateGoal(int type, int value)
    {
        _goals[type] -= value;
        if (_goals[type] < 0)
        {
            _goals[type] = 0;
        }
        _goalTexts[type].text = $"x{_goals[type]}";

        for (int i = 0; i < _goals.Length; i++)
        {
            if (_goals[i] != 0) { return; }
        }
        Win();
    }

    private void Win()
    {
        _win.SetActive(true);
        Wallet.Value += 100;
        Levels.Max++;
    }

    private Sprite GetRandomSprite()
    {
        //return _enableBallSprite[0];
        return _enableBallSprite[Random.Range(0, _enableBallSprite.Length)];
    }
}
