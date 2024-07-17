using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterPump
{
    void Pump(int forcePump);
    void SetPropertyWater(EWaterProperty type);
}
