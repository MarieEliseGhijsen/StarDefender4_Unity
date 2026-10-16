using UnityEngine;

public class CircEnemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    private float t;        //forward speed along the Z axis
    public const float SPEED = 0.005f;

    public GameObject bulletPrefab;

    private float shootTimer;
    private const int SHOOT_TIMER_END = 1000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = 0.0f;

        shootTimer = Random.Range(0.0f, SHOOT_TIMER_END);
    }

    void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, rb.position, bulletPrefab.transform.rotation);
        Buulet script = bullet.GetComponent<Buulet>();
        script.isPlayerBullet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer > SHOOT_TIMER_END)
        {
            shootTimer = Random.Range(0.0f, SHOOT_TIMER_END);
            shoot();
        }
        else
        {
            shootTimer++;
        }

        if (t > 22)
        {
            t = 0.0f;
        }

        float x = t - (4 * Mathf.Sin(t));
        float z = 2 - (4 * Mathf.Cos(t));

        rb.position = new Vector3(x-10, 0.0f, z);
        t += SPEED;
    }
}