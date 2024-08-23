using UnityEngine;

public class FollowerToPlayer : MonoBehaviour
{
    Vector3 playerPosition;
    [SerializeField] float speed = 0.1f;
    [SerializeField] Transform player;


    void Start()
    {
        player.position = -transform.position;
    }

    void Update()
    {
        playerPosition = player.position;
        player.position.Normalize();
        transform.position = Vector2.Lerp(transform.position, -playerPosition/2, speed);
    }
}
