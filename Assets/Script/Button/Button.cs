using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{

    public KeyCode _forwardKey;

    private KeyCode _newKey;

    setButtonMap _command;

    [SerializeField]
    buttonMapInvoker _buttonInvoker;
    enum buttonType  {forward, backwards, left, right, shoot, jump };

    [SerializeField]
    private buttonType _buttonType;

    private bool canScan = false;
    private TextMeshProUGUI _buttonText;

    [SerializeField]
    InputHandler _inputHandler;


    // Start is called before the first frame update
    void Start()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _buttonText.text = _forwardKey.ToString();

        _buttonInvoker = FindObjectOfType<buttonMapInvoker>();

        _command = new setButtonMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScanKey()
    {
        UpdateCurrentKey();
        //_buttonText.text = _forwardKey.ToString();
        canScan = true;
    }

    private void OnGUI()
    {

        _buttonText.text = _forwardKey.ToString();
        UpdateCurrentKey();

        if (canScan)
        {
            foreach(KeyCode _key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(_key) && _key != KeyCode.None)
                {
                    Debug.Log(_key);
                    //_inputHandler.UpdateKeys(_forwardKey, _key);
                    _command.Execute(_inputHandler, _forwardKey, _key);
                    _buttonInvoker.addToStack(_command, _forwardKey, _key);


                    UpdateCurrentKey();
                    
                    canScan = false;
                }
            }
        }
        
    }

    private void UpdateCurrentKey()
    {
        if(_buttonType == buttonType.forward)
        {
            _forwardKey = _inputHandler._kForward;
        }
        else if(_buttonType == buttonType.backwards)
        {
            _forwardKey = _inputHandler._kBack;
        }
        else if (_buttonType == buttonType.left)
        {
            _forwardKey = _inputHandler._kLeft;
        }
        else if (_buttonType == buttonType.right)
        {
            _forwardKey = _inputHandler._kRight;
        }
        else if (_buttonType == buttonType.jump)
        {
            _forwardKey = _inputHandler._kJump;
        }
        else if (_buttonType == buttonType.shoot)
        {
            _forwardKey = _inputHandler._kShoot;
        }
    }
}
