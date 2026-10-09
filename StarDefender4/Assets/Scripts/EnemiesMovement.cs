using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class EnemiesMovement : MonoBehaviour
{
    //public Transform player;
    public float mass; //MIN : 10 ;;;; SMA : 0-100, MED : 100-200, LAR : 200-300

    private Rigidbody rb;
    private Vector3 dir;
    private float speed;

    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dir.x = Random.Range(-1.0f, 1.0f);
        dir.y = 0.0f;
        dir.z = Random.Range(-1.0f, 1.0f);

        speed = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < 2.0f)
        {
            rb.AddForce(dir * speed);
        }
        else
        {
            dir.x = Random.Range(-1.0f, 1.0f);
            dir.z = Random.Range(-1.0f, 1.0f);
            timer = 0.0f;
        }

        timer = timer + 0.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemiesMovement enemyScript = collision.gameObject.GetComponent<EnemiesMovement>();


            if (enemyScript != null)
            {
                if (enemyScript.mass < mass)
                {

                    Destroy(collision.gameObject);
                }
            }


        }
    }
}