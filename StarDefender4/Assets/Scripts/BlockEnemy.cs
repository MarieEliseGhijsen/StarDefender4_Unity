using System.Data;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    private int lives;

    private Rigidbody rb;
    private Vector3 dir;
    private Vector3 prevDir;
    public float speed;

    public GameObject bulletPrefab;

    private float spawnTimer;
    private const int SPAWN_TIMER_END = 600;
    private float sideTimer;
    private const int SIDE_MOVE_TIMER_END = 1000;
    private float shootTimer;
    private const int SHOOT_TIMER_END = 10000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lives = 2;

        dir.x = 0.0f;
        dir.y = 0.0f;
        dir.z = -1.0f;

        prevDir.x = 1.0f;
        prevDir.y = 0.0f;
        prevDir.z = 0.0f;

        speed = 5.0f;
        spawnTimer = 0.0f;
        sideTimer = SIDE_MOVE_TIMER_END + 1.0f;

        shootTimer = Random.Range(0.0f, SHOOT_TIMER_END);
    }

    void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, rb.position, bulletPrefab.transform.rotation);
        Buulet script = bullet.GetComponent<Buulet>();
        Vector3 bulletDir = new Vector3(0.0f, 0.0f, -1.0f);
        script.setBullet(false, bulletDir, false);
    }

    public void hurt(bool t_powerUp)
    {
        if(t_powerUp) lives = lives - 2;
        else lives = lives - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(lives <= 0)
        {
            Destroy(this.gameObject);
        }

        if (shootTimer > SHOOT_TIMER_END)
        {
            shootTimer = Random.Range(0.0f, SHOOT_TIMER_END);
            shoot();
        }
        else
        {
            shootTimer++;
        }

        if (spawnTimer > SPAWN_TIMER_END)
        {
            if (sideTimer > SIDE_MOVE_TIMER_END)
            {
                sideTimer = 0;

                if (dir.z != 0.0f)
                {
                    dir = new Vector3(prevDir.x * -1, 0, 0);
                    prevDir = new Vector3(dir.x, 0, 0);
                }
                else
                {
                    dir = new Vector3(0, 0, -1);
                }
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

        if (rb.position.z < -2.0f)
        {
            Destroy(this.gameObject);
        }
    }
}