using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    CameraFollow _cameraFollow;
    PlayerMovement _player;

    void Start()
    {
        _cameraFollow = FindObjectOfType<CameraFollow>();
        _player = FindObjectOfType<PlayerMovement>();
    }
    public void NextLevel()
    {
        _cameraFollow._isPaused = false;
        _cameraFollow.started = true;
        _player.started = true;
        SceneManager.LoadScene(2);
    }
}
