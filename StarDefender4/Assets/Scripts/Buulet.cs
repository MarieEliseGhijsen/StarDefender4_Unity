using UnityEngine;

public class Buulet : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    public float speed;

    public bool isPlayerBullet;
    public Material enemySkin;
    public Material humanSkin;

    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ///dir = new Vector3(0.0f, 0.0f, -1.0f);
        speed = 10.0f;
    }

    public void setBullet(bool t_isPlayerBullet, Vector3 t_dir)
    {
        Renderer skin = GetComponent<Renderer>();

        if (t_isPlayerBullet)
        {
            isPlayerBullet = true;
            skin.material = humanSkin;
        }
        else
        {
            isPlayerBullet = false;
            skin.material = enemySkin;
        }

        dir = t_dir;
    }

    private void OnTriggerEnter(UnityEngine.Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPlayerBullet)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            if (player)
                player.hurt();
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("Enemy") && isPlayerBullet)
        {
            //PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            player.scorePlusPlus();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir * speed);

        if(rb.position.z < -2.0f)
        {
            Destroy(this.gameObject);
        }
    }
}