using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : singleton<ScoreUI>, EnemyObserver
{
    public int score = 0;
    public int maxScore = 3;

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
        //score = 
        //GUILayout.BeginArea(
        //    new Rect(50, 50, 100, 2000));
        //GUILayout.BeginHorizontal("box");
        //GUILayout.Label("score: "+score);
        //GUILayout.EndHorizontal();
        //GUILayout.EndArea();

        scoreText.text = score.ToString();
    }
}
