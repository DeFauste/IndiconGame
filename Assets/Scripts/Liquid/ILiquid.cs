using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Liquid
{
    public interface ILiquid
    {
        // void Pump(int forcePump);
        // float Fresh(int forcePump, float V);
        // bool SetPropertyWater(EWaterProperty type);
        // float GetSquare();
        EWaterProperty Property { get; set; }
        float SubSquare();
        float GetSquare();
        void ResizeSquare();
        float Pump(int forcePump);
        float Fresh(int forcePump, float square);
    }
}
