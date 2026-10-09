using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public GameObject scoreTextObject;
    public GameObject livesTextObject;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;

    int score;
    int lives;

    //private MonoBehaviour enemyScript = GetMonoBehaviour()

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        score = 0;
        SetScoreText();
        lives = 3;
        SetLivesText();
        winTextObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //EnemiesMovement enemyScript = collision.gameObject.GetComponent<EnemiesMovement>();
            //if (enemyScript != null)
            //{
            //    if (enemyScript.mass > mass)
            //    {
            //        winTextObject.SetActive(true);
            //        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            //        Destroy(GameObject.FindGameObjectWithTag("Player"));
            //    }
            //    else
            //    {
            //        SetScoreText();
            //        Destroy(collision.gameObject);
            //    }
            //}

        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
    }

    void SetScoreText()
    {
        scoreTextObject.GetComponent<TextMeshProUGUI>().text = "Score : " + (score).ToString();
    }

    void SetLivesText()
    {
        livesTextObject.GetComponent<TextMeshProUGUI>().text = "Lives : " + (lives).ToString();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);

        rb.AddForce(movement * speed);

        //if(!GameObject.FindGameObjectWithTag("Enemy"))
        //{
        //    winTextObject.SetActive(true);
        //    winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}