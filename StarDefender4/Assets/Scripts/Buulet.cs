using UnityEngine;

public class Buulet : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = new Vector3(0.0f, 0.0f, -1.0f);
        speed = 10.0f;
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
