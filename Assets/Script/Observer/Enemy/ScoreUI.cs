using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : singleton<ScoreUI>, EnemyObserver
{
    public int score = 0;
    public int maxScore = 3;

    private bool _isDirty = true;
    [SerializeField]
    TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();
    }
    public void OnHealthChanged(int newHealth)
    {
        Debug.Log($"Enemy health changed: {newHealth}");
        // Optionally update the UI with new health
    }

    public void OnEnemyDeath()
    {
        score += Random.Range(1, maxScore);
        //Debug.Log($"Enemy defeated! Score: {score}");
        // Update the score UI here if needed
    }

    public void SetDirty()
    {
        _isDirty = true;
    }

    private void Update()
    {
        if (_isDirty)
        {
            scoreText.text = score.ToString();
            _isDirty = false;
        }
    }
}
