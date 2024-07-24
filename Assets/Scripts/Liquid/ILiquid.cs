using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Liquid
{
    public interface ILiquid
    {
        EWaterProperty Property { get; set; }
        bool SetPropertyWater(EWaterProperty property);
        float GetSquare();
        float Pump(int forcePump);
        float Squeeze(int forcePump, float square);
    }
}
