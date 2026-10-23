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

    int lives;

    public GameObject dieAudioPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lives = 2;
        t = Random.Range(0.0f, 22.0f);

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
        if (t_powerUp) lives = lives - 2;
        else lives = lives - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            Instantiate(dieAudioPrefab, new Vector3(0.0f, 0.0f, 0.0f), dieAudioPrefab.transform.rotation);
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