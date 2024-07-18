using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    Color color;
    [SerializeField] Color waterColor;
    [SerializeField] Color slimeColor;
    [SerializeField] Color gasolineColor;
    [SerializeField] Color lemonadeColor;
    SpriteRenderer sprite;
    private void OnEnable() => PlayerPump.ActionWaterProperty += OnPropertyChange;
    private void OnDisable() => PlayerPump.ActionWaterProperty -= OnPropertyChange;

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
        // else if (property == EWaterProperty.Lemonade)
        // {
        //     sprite.color = lemonadeColor;
        // }

    }
}
