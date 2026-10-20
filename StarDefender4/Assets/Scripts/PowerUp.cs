using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    private float speed;

    public bool active = false;
    private float spawnTimer;
    private const int SPAWN_TIMER_END = 1000;

    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = new Vector3(0.0f, 0.0f, -1.0f);
        speed = 3.0f;
        spawnTimer = Random.Range(0.0f, SPAWN_TIMER_END);
    }

    private void OnTriggerEnter(UnityEngine.Collider collision)
    {
        if (!active) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            active = false;

            player = collision.gameObject.GetComponent<PlayerController>();
            if (player)
                player.powerUP();
        }
    }

        // Update is called once per frame
    void Update()
    {
        if(active)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(dir * speed);

            if(rb.position.z <= 0.0f)
            {
                active = false;
            }
        }
        else
        {
            if(spawnTimer > SPAWN_TIMER_END)
            {
                active = true;
                spawnTimer = Random.Range(0.0f, SPAWN_TIMER_END);

                rb.position = new Vector3(Random.Range(-9.0f, 9.0f), 0.0f, 5.0f);
            }
            else
            {
                spawnTimer = spawnTimer + 1;
            }
        }
    }
}