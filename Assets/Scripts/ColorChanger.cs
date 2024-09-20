using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine;
using Assets.Scripts.Liquid;
using Assets.Scripts.Player;

public class ColorChanger : MonoBehaviour
{
    Color color;
    [SerializeField] Color waterColor;
    [SerializeField] Color slimeColor;
    [SerializeField] Color gasolineColor;
    [SerializeField] Color sodaColor;
    SpriteRenderer sprite;
    private void OnEnable() => PlayerPumpLiquid.ActionLiquidProperty += OnPropertyChange;
    private void OnDisable() => PlayerPumpLiquid.ActionLiquidProperty -= OnPropertyChange;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    private void OnPropertyChange(EWaterProperty property)
    {
        if (property == EWaterProperty.Slime)
        {
            sprite.color = slimeColor;
        }
        else if (property == EWaterProperty.Gasoline)
        {
            sprite.color = gasolineColor;
        }
        else if (property == EWaterProperty.Water)
        {
            sprite.color = waterColor;
        }
        else if (property == EWaterProperty.None)
        {
            sprite.color = color;
        }
        else if (property == EWaterProperty.Soda)
        {
            sprite.color = sodaColor;
        }

    }
}
