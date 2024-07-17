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
        float V = GetSquare();
        Vector3 yP ;
        Vector3 yS ;
        if (square >= 0f)
        {
            float bust = 8/(24*transform.localScale.x);
            Debug.Log($"BUST = {bust}");
            gameObject.SetActive(true);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.03f * forcePump* bust * Time.fixedDeltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.01f * forcePump* bust * Time.fixedDeltaTime, transform.localScale.z);

        }
        return GetSquare()-V;
    }
    public override float Pump(int forcePump)
    {
        float getV = 0f;
        if (startSquare >= 0)
        {
            if((startSquare - forcePump) > 0) {
                Debug.Log($"{startSquare}");
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
