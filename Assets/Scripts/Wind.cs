using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string tag;
    [SerializeField] protected Vector2 velocity = Vector2.zero;
    private GameObject gameObject;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(tag))
        {
            Debug.Log("yes");

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
