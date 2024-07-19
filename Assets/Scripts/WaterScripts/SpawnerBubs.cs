using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBubs : MonoBehaviour
{
    [SerializeField] private GameObject bub;
    [SerializeField] private GameObject Linquid;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float Interwal = 1;
    [SerializeField] private int countBubs;
    [SerializeField] private float speedFlyBub = 1;
    [SerializeField] private float SizeBub = 1;
    [SerializeField] private float TimeLifeBubTouch = 3;
    [SerializeField] private float HeightBubFly = 7;
    private List<GameObject> bubsList = new List<GameObject>();
    private IWaterIneract interactLinquid;
    private bool EnableBub = false;
    // Start is called before the first frame update
    void Start()
    {
        interactLinquid = Linquid.GetComponent<IWaterIneract>();
        countBubs = (int)(Mathf.Abs(rightPoint.position.x - leftPoint.position.x)/ Interwal);
    }

    private void FixedUpdate()
    {
        if (interactLinquid != null && interactLinquid.Property == EWaterProperty.Soda && interactLinquid.GetSquare() > 0f)
        {
            if (bubsList.Count == 0) GenerateBub();
            OffOnBubs(true);
        } else
        {
            if(EnableBub)
                OffOnBubs(false);  
        }
    }

    private void GenerateBub()
    {
        EnableBub = true;
        for (int i = 1; i <= countBubs; i++)
        {
            GameObject b = Instantiate(bub);
            b.transform.position = new Vector3(leftPoint.position.x + i*Interwal, leftPoint.position.y, leftPoint.position.z);
            b.transform.localScale = new Vector3(SizeBub, SizeBub);
            b.GetComponent<BubFly>().Construct(speedFlyBub, TimeLifeBubTouch, HeightBubFly);
            bubsList.Add(b);
        }
    }

    private void OffOnBubs(bool state)
    {
        EnableBub = state;
        for (int i = 0; i < countBubs; i++)
        {
            bubsList[i].SetActive(state);
            bubsList[i].transform.position = new Vector3(leftPoint.position.x + (i+1) * Interwal, leftPoint.position.y, leftPoint.position.z);   
        }
    }
}
