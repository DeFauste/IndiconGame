using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubFly : MonoBehaviour
{
    [SerializeField] private float _speedFly = 1;
    [SerializeField] private float _timeLifeTouch = 3;
    [SerializeField] private float _heightFlyY = 3;
    private float _startPosY;
    private float _maxPosY;
    private Collider2D _collider;
    private SpriteRenderer _sprite;
    private bool _isMove = true;
    private bool _isTouch = false;
    private bool _currState = true;
    public void Construct(float speedFly, float timeLifeTouch, float heightFlyY)
    {
        _speedFly = speedFly;
        _timeLifeTouch = timeLifeTouch;
        _heightFlyY = heightFlyY;
        _maxPosY = _startPosY + _heightFlyY;
    }

    public void State(bool state)
    {
        if (_currState != state) {
            _currState = state;
            _collider.enabled = state;
            _sprite.enabled = state;
            _isMove = state;
        }
    }
    void Start()
    {
        _startPosY = gameObject.transform.position.y;
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
    //�������� �� ������� ����� ������ Groundcheck
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Foot" && !_isTouch)
    //    {
    //        _isTouch = true;
    //        StartCoroutine(TimerDestroyBub());
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_isTouch)
        {
            Debug.Log("YEP");
            _isTouch = true;
            StartCoroutine(TimerDestroyBub());
        }
    }
    IEnumerator TimerDestroyBub()
    {
        yield return new WaitForSeconds(_timeLifeTouch);
        _collider.enabled = false;
        _sprite.enabled = false;
        _isTouch = false;


    }
    private void Move()
    {
        float y = transform.position.y + _speedFly* Time.fixedDeltaTime;
        gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
        if(gameObject.transform.position.y > _maxPosY)
        {
            StartCoroutine(Respawn());
        }
    }
    IEnumerator Respawn()
    {
        _isMove = false;
        _collider.enabled = false;
        _sprite.enabled = false;
        yield return new WaitForSeconds(1);
        gameObject.transform.position = new Vector3(transform.position.x, _startPosY, transform.position.z);
        _isMove = true;
        _collider.enabled = true;
        _sprite.enabled = true;
    }
}
