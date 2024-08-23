using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Liquid
{
    public class SpinLiquid : MonoBehaviour, ILiquid
    {
        [SerializeField] private GameObject gameObject;
        [SerializeField] private WaterShapeController waterScripts;
        [SerializeField] private Canvas hint;
        private ILiquid liquidInteract;

        private EWaterProperty _typeSpring = EWaterProperty.None;
        public EWaterProperty Property { get => _typeSpring; set { SetPropertyWater(value); } }

        private void Start()
        {
            liquidInteract = gameObject.GetComponent<ILiquid>();
            _typeSpring = liquidInteract.Property;
            hint.enabled = false;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            IPumpProperty i = collision.gameObject.GetComponent<IPumpProperty>();
            if (!hint.enabled && i != null && i.GetSquare > 0 && (i.GetProperty == _typeSpring ||  _typeSpring == EWaterProperty.None))
            {
                hint.enabled = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            hint.enabled = false;
        }

        public float Squeeze(int forcePump, float square)
        {

            return liquidInteract.Squeeze(forcePump, square);         
        }

        public float GetSquare()
        {
            return liquidInteract.GetSquare();
        }

        public float Pump(int forcePump)
        {
            return liquidInteract.Pump(forcePump);
        }

        public bool SetPropertyWater(EWaterProperty type)
        {
            if (liquidInteract.GetSquare() == 0 || type == _typeSpring || _typeSpring == EWaterProperty.None)
            {
                _typeSpring = type;
                liquidInteract.Property = type;
                return true;
            }

            return false;
        }
    }
}
