using UnityEngine;

public class PlayerInteracte : MonoBehaviour
{
    PlayerInput _inputActions;

    IWaterPump _interacteble;
    private void Start()
    {
        _inputActions = new PlayerInput();
        _inputActions.Enable();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IWaterPump i =  collision.GetComponent<IWaterPump>();
        if(i != null && _interacteble == null)
        {
            _interacteble = i;
        }
    }

    private void Update()
    {
        if(_interacteble != null && Input.GetKey(KeyCode.F))
        {
            
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
