using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSizeChange : MonoBehaviour, IInteracteble
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private WaterShapeController waterScripts;
    private Transform transform;
    [SerializeField] private Canvas hint;
    private void Start()
    {
        hint.enabled = false;
        transform = gameObject.transform;
    }

    public void Interacte()
    {
        gameObject.SetActive(true);

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.06f * Time.deltaTime, transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.02f * Time.deltaTime, transform.localScale.z);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hint.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hint.enabled = false;
    }
}
