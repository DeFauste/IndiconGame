using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

namespace Assets.Scripts.Liquid
{
    public class UniversumLiquid : MonoBehaviour, ILiquid
    {
        [SerializeField] public EWaterProperty property;
        Color color;
        [SerializeField] Color waterColor;
        [SerializeField] Color slimeColor;
        [SerializeField] Color gasoColor;
        [SerializeField] Color sodaColor;
        [SerializeField] SpriteShapeRenderer sprite;
        [SerializeField] private float startSquare;

        public EWaterProperty Property { get => property; set
            {
                property = value;
                SetColorProperty();
            }
        }
        void Start()
        {
            if (sprite != null)
            {
                SetColorProperty();
                startSquare = GetSquare();
            }
        }
        private void SetColorProperty()
        {
            if (Property == EWaterProperty.None)
            {
                color = Color.white;
            }
            else if (Property == EWaterProperty.Water)
            {
                color = waterColor;
            }
            else if (Property == EWaterProperty.Slime)
            {
                color = slimeColor;

            }
            else if (Property == EWaterProperty.Gasoline)
            {
                color = gasoColor;
            }
            else if (Property == EWaterProperty.Soda)
            {
                color = sodaColor;
            }
            sprite.color = color;
        }

        public float Fresh(int forcePump, float square)
        {
            //TO DO 
            return 0f;
        }

        public float GetSquare()
        {
            return 24 * 8 * gameObject.transform.localScale.x * gameObject.transform.localScale.y;
        }

        public float Pump(int forcePump)
        {
            float getV = 0f;
            if (startSquare >= 0)
            {
                if ((startSquare - forcePump) > 0)
                {
                    getV = forcePump;
                    startSquare -= forcePump;
                    float s = startSquare / (24 * transform.localScale.x * 8);
                    transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y - s) * 3f, transform.position.z);
                    transform.localScale = new Vector3(transform.localScale.x, s, transform.localScale.z);
                }
                else
                {
                    float s = startSquare / (24 * transform.localScale.x * 8);
                    getV = startSquare;
                    transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y - s) * 3f, transform.position.z);
                    startSquare = 0;
                    transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
                    gameObject.SetActive(false);

                }
            }

            return getV;
        }
    }
}
