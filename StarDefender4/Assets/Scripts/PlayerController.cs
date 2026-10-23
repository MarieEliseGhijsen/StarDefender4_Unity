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
    public Material superFlash;
    public Material superHuman;

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

    public bool powerUp;
    float powerUpTimer;
    const float POWERUP_TIMER_END = 350.0f;

    //private MonoBehaviour enemyScript = GetMonoBehaviour()

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        invinsibleTimer = 0.0f;
        powerUpTimer = 0.0f;

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
        FlashChange();
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
        FlashChange();
    }

    //void OnMove(InputValue movementValue)
    //{
    //    Vector2 movementVector = movementValue.Get<Vector2>();
    //    movementX = movementVector.x;

    //    int shot = (int)movementVector.y;
    //    justShot = shot != 0;
    //}

    private void FlashChange()
    {
        Renderer skin = GetComponent<Renderer>();

        if (invinsible && !powerUp)
        {
            skin.material = flash;
        }
        else if(!invinsible && !powerUp)
        {
            skin.material = human;
        }
        else if(invinsible && powerUp)
        {
            skin.material = superFlash;
        }
        else if (!invinsible && powerUp)
        {
            skin.material = superHuman;
        }

        Debug.Log(skin);
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
            FlashChange();
        }
        else if (invinsible)
        {
            invinsibleTimer += Time.deltaTime;
        }

        if(powerUp && powerUpTimer >= POWERUP_TIMER_END)
        {
            powerUp = false;
            powerUpTimer = 0.0f;
            FlashChange();
        }
        else if(powerUp)
        {
            powerUpTimer++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}