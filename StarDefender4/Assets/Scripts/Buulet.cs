using UnityEngine;

public class Buulet : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    public float speed;

    public bool isPlayerBullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = new Vector3(0.0f, 0.0f, -1.0f);
        speed = 10.0f;

        isPlayerBullet = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPlayerBullet)
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.hurt();
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("Enemy") && isPlayerBullet)
        {
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