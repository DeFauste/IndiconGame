using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWaterProperty
{
    None,
    Water,
    Slime,
    Gasoline,
    Soda
}
public interface IWaterIneract
{
    EWaterProperty Property { get; set; }
    float SubSquare();
    void ResizeSquare();
    float Pump(int forcePump, float V);
}
public abstract class WaterProperty : MonoBehaviour, IWaterIneract
{

    [SerializeField] public EWaterProperty property;

    public abstract EWaterProperty Property { get; set; }

    public abstract float Pump(int forcePump, float V);
    public abstract void ResizeSquare();
    public abstract float SubSquare();
}
