using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSizeChange : MonoBehaviour, IWaterPump
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private WaterShapeController waterScripts;
    private Transform transform;
    [SerializeField] private Canvas hint;
    private EWaterProperty _type = EWaterProperty.None;
    IWaterIneract waterIneract;
    private void Start()
    {
        hint.enabled = false;
        transform = gameObject.transform;
        waterIneract = gameObject.GetComponent<IWaterIneract>();
    }

    public void Pump(int forcePump)
    {
        gameObject.SetActive(true);
        waterIneract.Pump(forcePump);
        waterIneract.Property = _type;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        hint.enabled = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hint.enabled = false;
        _type = EWaterProperty.None;
    }

    public void SetPropertyWater(EWaterProperty type)
    {
        _type = type;
    }

    public float GetSquare()
    {
        return waterIneract.GetSquare();
    }

    public float Fresh(int forcePump, float V)
    {
        return waterIneract.Fresh(forcePump, V);    
    }
}
