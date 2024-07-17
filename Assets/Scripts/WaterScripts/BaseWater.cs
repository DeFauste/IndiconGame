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
    private float startSquare;
    public override float Pump(int forcePump)
    {
        if (transform.localScale.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f*forcePump * Time.fixedDeltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.01f * forcePump  * Time.fixedDeltaTime, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x, 0 , transform.localScale.z);
            gameObject.SetActive(false);
        }
        return SubSquare();
    }

    public override void ResizeSquare()
    {
        startSquare = GetSquare();
    }
    public override float SubSquare()
    {
        return startSquare - (startSquare - GetSquare());
    }


    // Start is called before the first frame update
    void Start()
    {
      if(sprite != null)
        {
            SetColorProperty();
            startSquare = gameObject.transform.localScale.x * gameObject.transform.localScale.y;
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
        return gameObject.transform.localScale.x * gameObject.transform.localScale.y;
    }

    public override float Fresh(int forcePump, float square)
    {
        if (GetSquare() <= square)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.03f * forcePump * Time.fixedDeltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.01f * forcePump * Time.fixedDeltaTime, transform.localScale.z);
        }
        return GetSquare()*1.25f;
    }
}
