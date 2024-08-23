using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string tag;
    [SerializeField] protected Vector2 velocity = Vector2.zero;
    private GameObject gameObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(tag))
        {
            gameObject = collision.gameObject;

            float influence = gameObject.transform.localScale.x;

            if (influence < 1.3)
            {
                Rigidbody2D _rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
                _rigidbody2D.AddForce(velocity, ForceMode2D.Force);
            }
        }
    }
}
