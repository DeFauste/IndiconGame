using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterPump
{
    void Pump(int forcePump);
    float Fresh(int forcePump, float V);
    bool SetPropertyWater(EWaterProperty type);
    float GetSquare();
}
