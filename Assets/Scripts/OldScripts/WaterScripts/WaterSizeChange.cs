using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSizeChange : MonoBehaviour, IWaterPump
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private WaterShapeController waterScripts;
    [SerializeField] private Canvas hint;
    private EWaterProperty _type = EWaterProperty.None;
    IWaterIneract waterIneract;
    private void Start()
    {
        hint.enabled = false;
        //waterIneract = gameObject.GetComponent<IWaterIneract>();
        //_type = waterIneract.Property;
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
    }


    public float GetSquare()
    {
        return waterIneract.GetSquare();
    }

    public float Fresh(int forcePump, float V)
    {
        return waterIneract.Fresh(forcePump, V);
    }

    public bool SetPropertyWater(EWaterProperty type)
    {
        if(waterIneract.GetSquare() == 0 || type == _type || _type == EWaterProperty.None)
        {
            _type = type;
            waterIneract.Property = _type;
            return true;
        }

        return false;
    }
}
