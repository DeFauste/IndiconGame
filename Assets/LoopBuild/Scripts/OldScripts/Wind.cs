using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private Vector2 velocity = Vector2.zero;
    [SerializeField] private float scalePush = 1.3f;
    Collider2D _collision;
    Rigidbody2D _rb;
    public bool OnOff {  get; set; } = true;
    private void OnTriggerStay2D(Collider2D collision)
    {
       

        if (_collision == null && _collision != collision)
        {
            _rb = collision.gameObject.GetComponent<Rigidbody2D>();
            
        }
        if(_rb != null)
        {
            float influence = collision.gameObject.transform.localScale.x;

            if (influence < scalePush)
            {
                _rb.AddForce(velocity, ForceMode2D.Force);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _collision = null;
        _rb = null;
    }
}
