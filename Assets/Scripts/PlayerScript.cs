using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    private int scoreValue = 0;
    private int livesValue = 3;
    private bool facingRight = true;
    private bool isOnGround;

    public float speed;
    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    public int level = 1;
    public GameObject[] enemies;

    Animator anim;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void Update()
    {
        SetScoreText();
        SetLivesText();

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    
    void SetScoreText()
    {
        score.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 8 && level == 2)
        {
            winTextObject.SetActive(true);

            for (int i=0; i<enemies.Length; i++)
            {
                Destroy(enemies[i].gameObject);
            }
        }
        if (scoreValue >= 4 && level == 1)
        {
            transform.position = new Vector2(66.9f, 0.0f);
            lives.text = livesValue.ToString();
            livesValue = 3;
            level = 2;
        }
    }

    void SetLivesText()
    {
        lives.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }
        if (scoreValue == 4 && level == 2)
        {
            loseTextObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
