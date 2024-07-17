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
    float GetSquare();
    void ResizeSquare();
    float Pump(int forcePump);
    float Fresh(int forcePump, float square);
}
public abstract class WaterProperty : MonoBehaviour, IWaterIneract
{

    [SerializeField] public EWaterProperty property;

    public abstract EWaterProperty Property { get; set; }

    public abstract float Fresh(int forcePump, float square );
    public abstract float GetSquare();
    public abstract float Pump(int forcePump);
    public abstract void ResizeSquare();
    public abstract float SubSquare();
}
