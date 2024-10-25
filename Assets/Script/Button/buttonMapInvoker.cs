using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonMapInvoker : MonoBehaviour
{

    [SerializeField]
    Stack<setButtonMap> _buttonMapstack = new Stack<setButtonMap>();
    Stack<KeyCode> _keyCodeStack = new Stack<KeyCode>();
    Stack<KeyCode> _newKeyCodeStack = new Stack<KeyCode>();

    InputHandler _inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.P))
        //{
        //    replay();
        //}

    }

    public void addToStack(setButtonMap command, KeyCode prevKey, KeyCode newKey)
    {
        _buttonMapstack.Push(command);
        _keyCodeStack.Push(prevKey);
        _newKeyCodeStack.Push(newKey);
    }

    public void replay()
    {
        if (_buttonMapstack.Count != 0)
        {
            _buttonMapstack.Peek().Execute(_inputHandler, _newKeyCodeStack.Peek(), _keyCodeStack.Peek());

            _buttonMapstack.Pop();
            _keyCodeStack.Pop();
            _newKeyCodeStack.Pop();
        }
        
    }

}
