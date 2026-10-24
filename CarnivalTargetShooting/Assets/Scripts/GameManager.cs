
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _roundDuration = 60f;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private TargetSpawner _targetSpawner;
    [SerializeField] private ScoreManager _scoreManager;

    private float _remainingTime;
    private bool _gameEnded;

    public static bool IsRoundActive { get; private set; }

    private void Start()
    {
        _remainingTime = _roundDuration;
        _gameEnded = false;
        IsRoundActive = true;

        // Hide the game-over screen at the beginning.
        _gameOverText.gameObject.SetActive(false);

        UpdateTimerText();
    }

    private void Update()
    {
        if (_gameEnded)
        {
            return;
        }

        // Update the countdown.
        _remainingTime -= Time.deltaTime;
        _remainingTime = Mathf.Max(0f, _remainingTime);

        UpdateTimerText();

        if (_remainingTime <= 0f)
        {
            EndGame();
        }
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(_remainingTime);
        _timerText.text = "Time: " + seconds;
    }

    private void EndGame()
    {
        _gameEnded = true;
        IsRoundActive = false;

        // Stop gameplay systems.
        _playerShoot.enabled = false;
        _targetSpawner.enabled = false;

        // Display the final result.
        _gameOverText.text = "GAME OVER\nFinal Score: " + _scoreManager.Score;

        _gameOverText.gameObject.SetActive(true);
    }
}

