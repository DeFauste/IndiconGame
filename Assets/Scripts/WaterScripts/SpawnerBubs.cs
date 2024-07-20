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
    [SerializeField] private int countBubs ;
    [SerializeField] private float speedFlyBub = 1;
    [SerializeField] private float SizeBub = 1;
    [SerializeField] private float TimeLifeBubTouch = 3;
    [SerializeField] private float HeightBubFly = 7;
    private List<GameObject> bubsList = new List<GameObject>();
    private IWaterIneract interactLinquid;
    private bool isGenerate = false;
    private bool isAcitvBubs = true;
    // Start is called before the first frame update
    void Start()
    {
        interactLinquid = Linquid.GetComponent<IWaterIneract>();
        Interwal = (Mathf.Abs(rightPoint.position.x - leftPoint.position.x) / countBubs);
    }

    private void FixedUpdate()
    {
        if (interactLinquid != null && interactLinquid.Property == EWaterProperty.Soda && interactLinquid.GetSquare() > 0f && !isGenerate)
        {
            isGenerate = !isGenerate;
            GenerateBub();

        }
        if(!Linquid.active && interactLinquid != null && (interactLinquid.Property != EWaterProperty.Soda || interactLinquid.GetSquare() <= 0))
        {
            OffOnBub(false);
        } else if(interactLinquid != null && interactLinquid.Property == EWaterProperty.Soda && interactLinquid.GetSquare() > 0)
        {
            OffOnBub(true);
        }
    }

    private void OffOnBub(bool state)
    {
        foreach (GameObject obj in bubsList)
        {
            obj.GetComponent<BubFly>().State(state);
        }
    }
    private void GenerateBub()
    {
        StartCoroutine(CreaterWait());
    }
    IEnumerator CreaterWait()
    {
        for (int i = 0; i < countBubs; i++)
        {
            yield return new WaitForSeconds(2);
            GameObject b = Instantiate(bub);
            b.transform.position = new Vector3(rightPoint.position.x - i*Interwal, leftPoint.position.y, leftPoint.position.z);
            b.transform.localScale = new Vector3(SizeBub, SizeBub);
            b.GetComponent<BubFly>().Construct(speedFlyBub, TimeLifeBubTouch, HeightBubFly);
            bubsList.Add(b);
        }
    }
}
