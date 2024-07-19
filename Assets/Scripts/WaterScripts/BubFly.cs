using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubFly : MonoBehaviour
{
    [SerializeField] private float _speedFly = 1;
    [SerializeField] private float _timeLifeTouch = 3;
    [SerializeField] private float _heightFlyY = 3;
    private float _startPosX;
    private float _maxPosX;
    private Collider2D _collider;
    private SpriteRenderer _sprite;
    private bool _isMove = true;
    public void Construct(float speedFly, float timeLifeTouch, float heightFlyY)
    {
        _speedFly = speedFly;
        _timeLifeTouch = timeLifeTouch;
        _heightFlyY = heightFlyY;
        _maxPosX = _startPosX + _heightFlyY;
    }
    void Start()
    {
        _startPosX = gameObject.transform.position.x;
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            Move();
        }
    }
    private void Move()
    {
        float y = transform.position.y + _speedFly* Time.fixedDeltaTime;
        gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
        if(gameObject.transform.position.y > _maxPosX)
        {
            Debug.Log("Corutina");
            StartCoroutine(Respawn());
        }
    }
    IEnumerator Respawn()
    {
        _isMove = false;
        _collider.enabled = false;
        _sprite.enabled = false;
        yield return new WaitForSeconds(Random.Range(1, 3));
        _isMove = true;
        _collider.enabled = true;
        _sprite.enabled = true;
    }
}
