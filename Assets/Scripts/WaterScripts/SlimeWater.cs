using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D;

public class SlimeWater : WaterProperty
{
    public override EWaterProperty Property { get => property; set => property = value; }
    [SerializeField] Color color = Color.blue;
    [SerializeField] SpriteShapeRenderer sprite;

    public override float Pump(int forcePump)
    {
        int pumpSize = 0;
        if (transform.localScale.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f*forcePump * Time.deltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.01f * forcePump * Time.deltaTime, transform.localScale.z);
            pumpSize = 1;
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x, 0 , transform.localScale.z);
            gameObject.SetActive(false);
        }

        return pumpSize;
    }

    public override float Square()
    {
        return gameObject.transform.localScale.x * gameObject.transform.localScale.y;
    }


    // Start is called before the first frame update
    void Start()
    {
      if(sprite != null)
        {
            sprite.color = color;
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
