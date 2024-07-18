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
    private List<GameObject> bubsList = new List<GameObject>();
    private IWaterIneract interactLinquid;
    // Start is called before the first frame update
    void Start()
    {
        interactLinquid = Linquid.GetComponent<IWaterIneract>();
        countBubs = (int)(Mathf.Abs(rightPoint.position.x - leftPoint.position.x)/ Interwal);
    }

    private void FixedUpdate()
    {
        if (interactLinquid != null && interactLinquid.Property == EWaterProperty.Soda)
        {
            if (bubsList.Count == 0) GenerateBub();
            else
            {
                Move();
            }
        }
    }

    private void GenerateBub()
    {
        for (int i = 1; i <= countBubs; i++)
        {
            GameObject b = Instantiate(bub);
            b.transform.position = new Vector3(leftPoint.position.x + i*Interwal, leftPoint.position.y, leftPoint.position.z);
            bubsList.Add(b);
        }
    }

    private void Move()
    {
        foreach (GameObject b in bubsList)
        {
            b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y + speedFlyBub * Time.fixedDeltaTime, b.transform.position.z);
        }
    }
}
