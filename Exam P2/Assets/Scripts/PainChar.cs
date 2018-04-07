using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainChar : MonoBehaviour {

    public float maxVel = 5f;
    public float yJumpForce = 300f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 jumpForce;
    private bool isJumping = false;
    private bool movingRight = true;
    private GameObject health;

    public float healthAmount;

    public int score;
    public Text scoreText;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = GameObject.Find("HealthHold");
        jumpForce = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update() {
        //We update horizontal speed
        float v = Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(0, rb.velocity.y); //Mono cambia de fuerzas

        v *= maxVel;

        vel.x = v; //Vector con velocidad horizontal calculada

        rb.velocity = vel;

        //We change animations if needed
        if (v != 0) {
            anim.SetBool("IsWalking", true);
        } else {
            anim.SetBool("IsWalking", false);
        }

        //if the player jumps
        if (Input.GetAxis("Jump") > 0.01f) {
            if (!isJumping) {
                if (rb.velocity.y == 0) {
                    isJumping = true;
                    anim.SetBool("IsJumping", true);
                    jumpForce.x = 0f;
                    jumpForce.y = yJumpForce;
                    rb.AddForce(jumpForce);
                }
            }
        } else {
            isJumping = false;
            if (rb.velocity.y == 0) {
                anim.SetBool("IsJumping", false);
            }
        }

        if (movingRight && v < 0) {
            movingRight = false;
            Flip();
        } else if (!movingRight && v > 0) {
            movingRight = true;
            Flip();
        }
        ShowScore();
    }

    private void Flip() {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            health.SendMessage("TakeDamage", 15);
        } else if (collision.gameObject.tag == "Health") {
            Destroy(collision.gameObject);
            health.SendMessage("Regeneration", healthAmount);
        } else if (collision.gameObject.tag == "CrystalB") {
            Destroy(collision.gameObject);
            score += 1;
        } else if (collision.gameObject.tag == "CrystalP") {
            Destroy(collision.gameObject);
            score += 2;
        } else if (collision.gameObject.tag == "CrystalR") {
            Destroy(collision.gameObject);
            score += 5;
        } else if (collision.gameObject.tag == "CrystalT") {
            Destroy(collision.gameObject);
            score += 10;
        } else if (collision.gameObject.tag == "CrystalY") {
            Destroy(collision.gameObject);
            score += 15;
        } else if (collision.gameObject.tag == "Money") {
            Destroy(collision.gameObject);
            score += 15;
        } else if (collision.gameObject.tag == "Gem") {
            Destroy(gameObject);
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.LoadNextLevel();
        }

    }

    void ShowScore() {
        scoreText.text = ("Score: " + score);
    }
    
    public  void Reset() {
        score = 0;
    }
}
