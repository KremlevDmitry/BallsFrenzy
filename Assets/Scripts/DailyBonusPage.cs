using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusPage : MonoBehaviour
{
    [SerializeField]
    private Transform[] _targets = default;
    [SerializeField]
    private Transform[] _rewards = default;
    [SerializeField]
    private Transform _ball = default;
    [SerializeField]
    private Button _takeButton = default;
    [SerializeField]
    private Text _rewardText = default;

    [SerializeField]
    private AnimationCurve _yCurve = default;


    private void Start()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        yield return new WaitForSecondsRealtime(.5f);
        int current = 0;
        int prev = 0;
        yield return StartCoroutine(Step(_targets[prev + current].position));
        prev++;
        for (int i = 1; i < 6; i++)
        {
            current = Random.Range(current, current + 2);
            yield return StartCoroutine(Step(_targets[prev + current].position));
            prev += i + 1;
        }
        current = Random.Range(current - 1, current + 1);
        current = Mathf.Max(current, 0);
        current = Mathf.Min(current, _rewards.Length - 1);
        yield return StartCoroutine(Step(_rewards[current].position));
        var position = _rewardText.transform.position;
        position.x = _rewards[current].position.x;
        _rewardText.transform.position = position;
        int reward = Random.Range(10, 100);
        _rewardText.text = $"+{reward}";
        _rewardText.enabled = true;
        DailyBonus.GetBonus();
        Wallet.Value += reward;
        _ball.gameObject.SetActive(false);
        _takeButton.gameObject.SetActive(true);
    }

    private IEnumerator Step(Vector3 target)
    {
        float time = .5f;
        var startPosition = _ball.position;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            var position = Vector3.Lerp(startPosition, target, t / time);
            position.y = Mathf.LerpUnclamped(startPosition.y, target.y, _yCurve.Evaluate(t / time));
            _ball.position = position;
            yield return null;
        }
    }
}
