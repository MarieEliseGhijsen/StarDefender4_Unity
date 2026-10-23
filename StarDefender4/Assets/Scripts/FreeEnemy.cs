using UnityEditor;
using UnityEngine;

public class FreeEnemy : MonoBehaviour
{
    private int lives;

    public GameObject dieAudioPrefab;

    private Rigidbody rb;
    private Vector3 dir;
    private float speed;
    private Vector3 currentPos;
    private Vector3 destinationPos;

    public GameObject bulletPrefab;

    private float dirrTimer;
    private const int DIRR_TIMER_END = 1000;
    private float shootTimer;
    private const int SHOOT_TIMER_END = 10000;

    private const int MIN_LOCATION_X = -9;
    private const int MAX_LOCATION_X = 9;

    private const int MIN_LOCATION_Z = 1;
    private const int MAX_LOCATION_Z = 6;

    private const float MIN_ANGLE = 120.0f;
    private const float MAX_AMGLE = 240.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lives = 2;

        shootTimer = Random.Range(0.0f, SHOOT_TIMER_END);

        //currentPos = new Vector3 (Random.Range(MIN_LOCATION_X, MAX_LOCATION_X),0.0f, Random.Range(MIN_LOCATION_Z, MAX_LOCATION_Z));
        //destinationPos = new Vector3 (Random.Range(MIN_LOCATION_X, MAX_LOCATION_X),0.0f, Random.Range(MIN_LOCATION_Z, MAX_LOCATION_Z));

        speed = 7.0f;
        dirrTimer = DIRR_TIMER_END;
    }


    void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, rb.position, bulletPrefab.transform.rotation);
        Buulet script = bullet.GetComponent<Buulet>();
        Vector3 bulletDir = new Vector3(Random.Range(-0.8f, 0.8f), 0.0f, -1.0f);
        script.setBullet(false, bulletDir, false);
    }
    public void hurt(bool t_powerUp)
    {
        if (t_powerUp) lives = lives - 2;
        else lives = lives - 1;
    }

    void changeDir()
    {
        dir = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
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

        //if (currentPos.x - destinationPos.x < 1)
        //{
        //    destinationPos = new Vector3(Random.Range(MIN_LOCATION_X, MAX_LOCATION_X), 0.0f, Random.Range(MIN_LOCATION_Z, MAX_LOCATION_Z));
        //}
        //else
        //{
        //    rb.position = Vector3.Lerp(currentPos, destinationPos, speed);
        //}

        if(dirrTimer > DIRR_TIMER_END)
        {
            dirrTimer = 0;

            changeDir();
        }
        else
        {
            dirrTimer++;
        }

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir * speed);

        if(rb.position.x > MAX_LOCATION_X + 0.1f)
        {
            rb.position = new Vector3(MIN_LOCATION_X, 0.0f, rb.position.z);
            dirrTimer = 0;
        }
        else if (rb.position.x < MIN_LOCATION_X - 0.1f)
        {
            rb.position = new Vector3(MAX_LOCATION_X, 0.0f, rb.position.z);
            dirrTimer = 0;
        }

        if (rb.position.z > MAX_LOCATION_Z + 0.1f)
        {
            rb.position = new Vector3(rb.position.x, 0.0f, MIN_LOCATION_Z);
            dirrTimer = 0;
        }
        else if (rb.position.z < MIN_LOCATION_Z - 0.1f)
        {
            rb.position = new Vector3(rb.position.x, 0.0f, MAX_LOCATION_Z);
            dirrTimer = 0;
        }
    }
}