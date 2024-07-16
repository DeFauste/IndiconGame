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

    private void OnTriggerStay2D(Collider2D collision)
    {
        IInteracteble i =  collision.GetComponent<IInteracteble>();
        if(i != null && _interacteble == null)
        {
            _interacteble = i;
        }
    }

    private void Update()
    {
        if(_interacteble != null && Input.GetKey(KeyCode.F))
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
