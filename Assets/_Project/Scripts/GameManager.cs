using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _totalTime = 120f;
    [SerializeField] private int _coinsToWin = 20;

    private float _timeRemaining;
    private int _currentCoins = 0;
    private bool _gameEnded = false;

    void Start()
    {
        _timeRemaining = _totalTime;

        UIManager.Instance.SetGoalData(_coinsToWin, _totalTime);
        UIManager.Instance.UpdateTimer(_timeRemaining);
    }

    void Update()
    {
        if (_gameEnded) return;

        _timeRemaining -= Time.deltaTime;
        _timeRemaining = Mathf.Max(_timeRemaining, 0f);

        UIManager.Instance.UpdateTimer(_timeRemaining);

        if (_timeRemaining <= 0f)
        {
            EndGame(false);
        }
    }

    public void CollectCoin(int amount)
    {
        if (_gameEnded) return;

        _currentCoins += amount;
        UIManager.Instance.AddCoins(amount);

        if (_currentCoins >= _coinsToWin)
        {
            EndGame(true);
        }
    }

    private void EndGame(bool won)
    {
        _gameEnded = true;
        Time.timeScale = 0f;

        if (won)
        {
            UIManager.Instance.ShowVictory();
        }
        else
        {
            UIManager.Instance.ShowGameOver();
        }
    }
}
