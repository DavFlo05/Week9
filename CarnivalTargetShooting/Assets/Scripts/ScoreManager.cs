
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _score;
    public int Score => _score;
    private void OnEnable()
    {
        Target.OnTargetHit += AddScore;
    }

    private void OnDisable()
    {
        Target.OnTargetHit -= AddScore;
    }

    private void AddScore(int points)
    {
        _score += points;

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = "Score: " + _score;
    }

    private void Start()
    {
        UpdateScoreText();
    }

}

