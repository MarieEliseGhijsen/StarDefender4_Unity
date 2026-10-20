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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject winText;

    public Material flash;
    public Material human;

    private Rigidbody rb;
    private float movementX;

    int score;
    int lives;

    bool justShot;

    bool invinsible;
    float invinsibleTimer;
    const float INVINSIBLE_TIMER_END = 2.0f;

    public GameObject bulletPrefab;

    private InputSystem_Actions controls;

    private bool powerUp;

    //private MonoBehaviour enemyScript = GetMonoBehaviour()

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        invinsibleTimer = 0.0f;

        score = 0;
        SetScoreText();

        lives = 3;
        SetLivesText();

        winText.SetActive(false);

        controls = new InputSystem_Actions();
        controls.Player.Enable();
        controls.Player.Attack.performed += shoot;

        powerUp = false;
    }

    void shoot(InputAction.CallbackContext context)
    {
        GameObject bullet = Instantiate(bulletPrefab, rb.position - new Vector3(0.0f, 0.0f, -1.0f), bulletPrefab.transform.rotation);
        Buulet script = bullet.GetComponent<Buulet>();
        Vector3 bulletDir = new Vector3 (0.0f, 0.0f, 1.0f);
        script.setBullet(true, bulletDir, powerUp);
        script.player = this;
    }

    public void powerUP()
    {
        powerUp = true;
        Debug.Log("Collected powerup");
    }

    public void scorePlusPlus()
    {
        score += 1;
        SetScoreText();
    }

    public void hurt()
    {
        if (invinsible)
        {
            return;
        }

        lives--;
        SetLivesText();

        invinsible = true;
        FlashChange(invinsible);
    }

    //void OnMove(InputValue movementValue)
    //{
    //    Vector2 movementVector = movementValue.Get<Vector2>();
    //    movementX = movementVector.x;

    //    int shot = (int)movementVector.y;
    //    justShot = shot != 0;
    //}

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
        scoreText.text = "Score : " + (score).ToString();
    }

    void SetLivesText()
    {
        livesText.text = "Lives : " + (lives).ToString();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveInput.x, 0.0f, 0.0f);


        rb.AddForce(movement * speed);

        if(lives <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            winText.SetActive(true);
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