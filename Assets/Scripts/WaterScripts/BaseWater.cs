using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.ParticleSystem;

public class BaseWater : WaterProperty
{
    public override EWaterProperty Property { get => property; 
        set {
            property = value;
            SetColorProperty();
            } 
        }
    [SerializeField] Color color = Color.blue;
    [SerializeField] SpriteShapeRenderer sprite;
    [SerializeField]private float startSquare;
    private float yStart;

    public override void ResizeSquare()
    {
        startSquare = GetSquare();
    }
    public override float SubSquare()
    {
        return (startSquare - GetSquare());
    }


    // Start is called before the first frame update
    void Start()
    {
      if(sprite != null)
        {
            SetColorProperty();
            startSquare = GetSquare();
            Debug.Log($"ֽאקאכüםûי מבתול:{startSquare}");
            yStart = transform.position.y;
        }      
    }

    private void SetColorProperty()
    {
        if(Property == EWaterProperty.None)
        {
            color = Color.white;
        } else if (Property == EWaterProperty.Water)
        {
            color = Color.blue;
        }
        else if (Property == EWaterProperty.Slime)
        {
            color = Color.green;

        }
        else if (Property == EWaterProperty.Gasoline)
        {
            color = Color.red;
        }
        sprite.color = color;
    }

    public override float GetSquare()
    {
        return 24*8*gameObject.transform.localScale.x * gameObject.transform.localScale.y;
    }

    public override float Fresh(int forcePump, float square)
    {
        float getV = 0f;

            if ((square-forcePump) > 0)
            {
                getV = forcePump;
                startSquare += forcePump;
                float s = startSquare / (24 * transform.localScale.x * 8);
                transform.position = new Vector3(transform.position.x, transform.position.y + (s - transform.localScale.y) * 3f, transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, s, transform.localScale.z);
                gameObject.SetActive(true);
            } else
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
    public override float Pump(int forcePump)
    {
        float getV = 0f;
        if (startSquare >= 0)
        {
            if((startSquare - forcePump) > 0) {
                getV = forcePump;
                startSquare -= forcePump;
                float s = startSquare / (24 * transform.localScale.x * 8);
                transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y-s)*3f, transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, s, transform.localScale.z);
            } else
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
