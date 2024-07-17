using UnityEngine;
using UnityEngine.U2D;

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
            yP = new Vector3(transform.position.x, transform.position.y + 0.03f * forcePump* bust * Time.fixedDeltaTime, transform.position.z);
            yS = new Vector3(transform.localScale.x, transform.localScale.y + 0.01f * forcePump* bust * Time.fixedDeltaTime, transform.localScale.z);

                transform.position = yP;
                transform.localScale = yS;
            

        }
        return GetSquare()-V;
    }
    public override float Pump(int forcePump)
    {
        float s = 0;
        if (transform.localScale.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f * forcePump * Time.fixedDeltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.01f * forcePump * Time.fixedDeltaTime, transform.localScale.z);
            s = SubSquare();
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            gameObject.SetActive(false);
        }
        return s;
    }
}
