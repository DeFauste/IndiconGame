using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject DropPrefab;
    [SerializeField] Transform point;

    private float cooldown = 0;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            cooldown -= Time.deltaTime;
            while (cooldown < 0)
            {
                cooldown += 0.01f;
                Instantiate(DropPrefab, (Vector2)point.position + Random.insideUnitCircle*.2f, Quaternion.identity);
            }
        }
    }
}
