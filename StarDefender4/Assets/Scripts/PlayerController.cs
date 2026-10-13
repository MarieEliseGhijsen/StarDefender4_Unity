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

    public Material flash;
    public Material human;

    private Rigidbody rb;
    private float movementX;

    int score;
    int lives;

    bool invinsible;
    float invinsibleTimer;
    const float INVINSIBLE_TIMER_END = 2.0f;

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
        invinsibleTimer = 0.0f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (invinsible)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
        {
            Renderer skin = GetComponent<Renderer>();
            lives--;
            SetLivesText();
            Destroy(collision.gameObject);

            invinsible = true;
            FlashChange(invinsible);
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
    }

    private void FlashChange(bool t_flash)
    {
        Renderer skin = GetComponent<Renderer>();

        if (t_flash)
        {
            skin.material = flash;
        }
        else
        {
            skin.material = human;
        }
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

        if(lives <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }

        if(invinsible && invinsibleTimer > INVINSIBLE_TIMER_END)
        {
            invinsible = false;
            invinsibleTimer = 0.0f;
            FlashChange(invinsible);
        }
        else if (invinsible)
        {
            invinsibleTimer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}