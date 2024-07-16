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
    float Square();

    float Pump(int forcePump);
}
public abstract class WaterProperty : MonoBehaviour, IWaterIneract
{

    [SerializeField] public EWaterProperty property;

    public abstract EWaterProperty Property { get; set; }

    public abstract float Pump(int forcePump);
    public abstract float Square();
}
