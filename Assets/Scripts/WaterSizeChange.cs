using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSizeChange : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    private Transform transform;
    private void Start()
    {
        transform = gameObject.transform;
    }
    public void Update()
    {
        if(Input.GetMouseButton(0))
        {
            gameObject.SetActive(true);
            
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.06f * Time.deltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.02f * Time.deltaTime, transform.localScale.z);
        }
    }
}
