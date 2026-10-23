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
    public CircEnemy circEnemy;
    public FreeEnemy freeEnemy;
    public BlockEnemy blockEnemy;

    private bool powerUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ///dir = new Vector3(0.0f, 0.0f, -1.0f);
        speed = 10.0f;
    }

    public void setBullet(bool t_isPlayerBullet, Vector3 t_dir, bool t_powerUp)
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
        
        powerUp = t_powerUp;
        dir = t_dir;
        //transform.rotation = Quaternion.Euler(0 * Mathf.Rad2Deg, dir.y * Mathf.Rad2Deg, -90);
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
        else if(collision.gameObject.CompareTag("Circ") && isPlayerBullet)
        {
            //PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            player.scorePlusPlus();
            circEnemy = collision.gameObject.GetComponent<CircEnemy>();
            if (circEnemy)
                circEnemy.hurt(powerUp);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Block") && isPlayerBullet)
        {
            //PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            player.scorePlusPlus();
            blockEnemy = collision.gameObject.GetComponent<BlockEnemy>();
            if (blockEnemy)
                blockEnemy.hurt(powerUp);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Free") && isPlayerBullet)
        {
            //PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            player.scorePlusPlus();
            freeEnemy = collision.gameObject.GetComponent<FreeEnemy>();
            if (freeEnemy)
                freeEnemy.hurt(powerUp);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Powerup") && isPlayerBullet)
        {
            PowerUp powerScript = collision.gameObject.GetComponent<PowerUp>();
            player.powerUP();
            powerScript.goInactive();
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