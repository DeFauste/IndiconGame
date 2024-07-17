using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public override float Pump(int forcePump, float V)
    {
        int pumpSize = 0;
        if (transform.localScale.y >= 0)
        {
            float bustOnV = V > 0 ? V/ (transform.localScale.x*100): 1;
            Debug.Log($"bust = {bustOnV}");
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f*forcePump* bustOnV * Time.deltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.01f * forcePump * bustOnV * Time.deltaTime, transform.localScale.z);
            pumpSize = 1;
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x, 0 , transform.localScale.z);
            gameObject.SetActive(false);
        }

        return pumpSize;
    }

    public override void ResizeSquare()
    {
        startSquare = gameObject.transform.localScale.x * gameObject.transform.localScale.y;
    }
    public override float SubSquare()
    {
        return startSquare - gameObject.transform.localScale.x * gameObject.transform.localScale.y;
    }


    // Start is called before the first frame update
    void Start()
    {
      if(sprite != null)
        {
            sprite.color = color;
            startSquare = gameObject.transform.localScale.x * gameObject.transform.localScale.y;
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
}
