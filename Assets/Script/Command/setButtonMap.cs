using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class setButtonMap
{

    
    
    public void Execute(InputHandler _inputHandler, KeyCode key, KeyCode newkey)
    {
        _inputHandler.UpdateKeys(key, newkey);
    }
}
