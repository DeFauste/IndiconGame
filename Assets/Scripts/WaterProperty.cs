using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWaterProperty
{
    Water,
    Slime,
    Gasoline,
    Soda
}
public interface IWaterIneract
{
    EWaterProperty Property { get; set; }
    float Square();
}
public abstract class WaterProperty : IWaterIneract
{

    [SerializeField] private EWaterProperty property;

    public abstract EWaterProperty Property { get; set; }

    public abstract float Square();
}
