using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public interface IPumpProperty
    {
        public float GetSquare { get; set; }
        public EWaterProperty GetProperty { get; set; }
    }
}
