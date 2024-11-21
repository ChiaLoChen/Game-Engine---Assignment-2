using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : singleton<ScoreUI>, EnemyObserver
{
    public int score = 0;
    public int maxScore = 3;

    private bool _isDirty = true;

    public int Score
    {
        get { return score; }
        set
        {
            if (score != value)
            {
                score = value;
                SetDirty();
            }
        }
    }
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
    void OnGUI()
    {
        scoreText.text = score.ToString();
    }

    private void SetDirty()
    {
        _isDirty = true;
    }

    private void Update()
    {
        if (_isDirty)
        {
            Debug.Log("Updating internal state with score: " + score);
            _isDirty = false;
        }
    }
}
