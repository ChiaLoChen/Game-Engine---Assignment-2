using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    InputHandler inputHandler;

    void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
    }
    
    public void Replay()
    {
        Debug.Log("Replay");
        inputHandler.Replay();
    }
}