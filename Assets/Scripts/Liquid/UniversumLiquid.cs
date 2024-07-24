using UnityEngine;
using UnityEngine.U2D;

namespace Assets.Scripts.Liquid
{
    public class UniversumLiquid : MonoBehaviour, ILiquid
    {
        [SerializeField] public EWaterProperty _property;
        Color color;
        [SerializeField] Color waterColor;
        [SerializeField] Color slimeColor;
        [SerializeField] Color gasoColor;
        [SerializeField] Color sodaColor;
        [SerializeField] SpriteShapeRenderer sprite;
        [SerializeField] private float startSquare;

        public EWaterProperty Property { get => _property; set
            {
                _property = value;
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

        public float Squeeze(int forcePump, float square)
        {
            float getV = 0f;

            if ((square - forcePump) > 0)
            {
                getV = forcePump;
                startSquare += forcePump;
                float s = startSquare / (24 * transform.localScale.x * 8);
                transform.position = new Vector3(transform.position.x, transform.position.y + (s - transform.localScale.y) * 3f, transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, s, transform.localScale.z);
                gameObject.SetActive(true);
            }
            else
            {
                getV = square;
                startSquare += square;
                float s = startSquare / (24 * transform.localScale.x * 8);
                transform.position = new Vector3(transform.position.x, transform.position.y + (s - transform.localScale.y) * 3f, transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, s, transform.localScale.z);
                gameObject.SetActive(true);
            }


            return getV;
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

        public bool SetPropertyWater(EWaterProperty property)
        {
            if (GetSquare() == 0 || _property == property || _property == EWaterProperty.None)
            {
                _property = property;
                return true;
            }

            return false;
        }
    }
}
