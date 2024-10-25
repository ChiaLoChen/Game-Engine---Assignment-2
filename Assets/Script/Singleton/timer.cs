using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class timer : singleton<timer>
{
    [SerializeField]
    private float _CurrentTime;
    private float _baseTime = 0.0f;
    private float _maxTime = 100.00f;

    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    GameObject loseScreen;

    [SerializeField]
    float _scoreGoal = 50f;

    public TextMeshProUGUI _timerText = null;
    float time;

    public enum timerType { timer, countdown };
    public timerType _timerType;

    public bool canCount = false;
    bool ended = false;


    // Start is called before the first frame update
    void Start()
    {

        _timerText = GameObject.Find("timerText").GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        if(_timerText == null)
        {
            
            _timerText = null;

            if (GameObject.Find("timerText"))
            {
                _timerText = GameObject.Find("timerText").GetComponent<TextMeshProUGUI>();
            }

        }
        //Debug.Log(_timerText);

        if(_timerText != null)
        {
            _timerText.text = time.ToString("0.0");

        }

        if (canCount)
        {
            switch (_timerType)
            {
                case timerType.timer:

                    _CurrentTime = _baseTime += Time.deltaTime;
                    time = _CurrentTime;

                    if (ScoreUI.Instance.score >= _scoreGoal)
                    {
                        ended = true;
                        winScreen.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }

                    break;
                case timerType.countdown:

                    _CurrentTime = _maxTime -= Time.deltaTime;
                    time = _CurrentTime;

                    if(time <= 0)
                    {
                        ended = true;
                        loseScreen.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                    if (ScoreUI.Instance.score >= _scoreGoal)
                    {
                        ended = true;
                        winScreen.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }

                    break;
            }

            if (ended)
            {
                canCount = false;
            }
        }
        
        


    }

    public void swapTimer(string type)
    {
        switch (type)
        {
            case "timer":
                _CurrentTime = 0;
                _baseTime = 0;
                _timerType = timerType.timer;
                break;
            case "countdown":
                _CurrentTime = 0;
                _maxTime = 100;
                _timerType = timerType.countdown;
                break;
        }
    }


    public float getTime()
    {
        return _CurrentTime;
    }
    void OnApplicationQuit()
    {
        
        Debug.Log("You have played: "+Math.Round(_CurrentTime, 2)+" seconds");
    }
}
