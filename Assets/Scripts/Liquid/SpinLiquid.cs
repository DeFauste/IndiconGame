using Assets.Scripts.Player;
using System;
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
        public EWaterProperty Property { get => _typeSpring; set
            {
                // TO DO
            }
        }


        private void Start()
        {
            liquidInteract = gameObject.GetComponent<ILiquid>();
            _typeSpring = liquidInteract.Property;
            hint.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IPumpProperty i = collision.gameObject.GetComponent<IPumpProperty>();
            if (i != null && i.GetSquare > 0 && (i.GetProperty == _typeSpring ||  _typeSpring == EWaterProperty.None))
            {
                hint.enabled = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            hint.enabled = false;
        }

        public float Fresh(int forcePump, float square)
        {
            //TO DO
            return 0f;
        }

        public float GetSquare()
        {
            //TO DO
            return 0f;
        }

        public float Pump(int forcePump)
        {
            //TO DO
            return 0f;
        }
    }
}
