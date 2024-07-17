using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterPump
{
    void Pump(int forcePump, float V);
    void SetPropertyWater(EWaterProperty type);
}
