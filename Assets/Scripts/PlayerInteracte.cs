using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracte : MonoBehaviour
{
    PlayerInput _inputActions;

    IInteracteble _interacteble;
    private void Start()
    {
        _inputActions = new PlayerInput();
        _inputActions.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteracteble i =  collision.GetComponent<IInteracteble>();
        if(i != null )
        {
            _interacteble = i;
        }
    }

    private void Update()
    {
        if(_interacteble != null && _inputActions.Gameplay.F.IsPressed())
        {
            _interacteble.Interacte();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _interacteble = null;
    }

    private void OnDestroy()
    {
        _inputActions.Disable();
    }
}
