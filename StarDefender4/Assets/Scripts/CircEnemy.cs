using UnityEngine;

public class CircEnemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 dir;
    private float t;        // Forward speed along the Z axis
    public const float SPEED = 0.005f;       
    




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = 0.0f;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t > 22)
        {
            t = 0.0f;
        }

        float x = t - (4 * Mathf.Sin(t));
        float z = 2 - (4 * Mathf.Cos(t));

        rb.position = new Vector3(x-10, 0.0f, z);


        t += SPEED;
        Debug.Log(rb.position);
    }
}
