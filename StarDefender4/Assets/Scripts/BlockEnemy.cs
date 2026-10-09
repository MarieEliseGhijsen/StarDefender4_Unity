using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    private Vector3 prevDir;
    public float speed;

    private float spawnTimer;
    private const int SPAWN_TIMER_END = 600;
    private float sideTimer;
    private const int SIDE_MOVE_TIMER_END = 1000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dir.x = 0.0f;
        dir.y = 0.0f;
        dir.z = -1.0f;

        prevDir.x = 1.0f;
        prevDir.y = 0.0f;
        prevDir.z = 0.0f;

        speed = 5.0f;
        spawnTimer = 0.0f;
        sideTimer = SIDE_MOVE_TIMER_END + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer > SPAWN_TIMER_END)
        {
            if (sideTimer > SIDE_MOVE_TIMER_END)
            {
                sideTimer = 0;

                prevDir = dir;

                dir = new Vector3(0.0f, 0.0f, -1.0f);

                rb.AddForce(dir * speed);

                if (prevDir.x == 0)
                {
                    int randDir = (int)Random.Range(0.0f, 1.0f);

                    if (randDir == 0) prevDir.x = -1.0f;
                    else prevDir.x = 1.0f;
                }

                dir = new Vector3(prevDir.x * -1, 0.0f, 0.0f);
            }
            else
            {
                sideTimer++;
            }
        }
        else
        {
            spawnTimer++;
        }

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir * speed);
    }
}