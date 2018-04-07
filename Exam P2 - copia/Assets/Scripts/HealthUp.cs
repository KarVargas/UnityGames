using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour {

    public float healthAmount;
    private GameObject health;

    void Start() {
        health = GameObject.Find("HealthHold");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            //playerHealth theHealth = collision.gameObject.GetComponent<playerHealth>();
            //theHealth.addHealth(healthAmount);
            health.SendMessage("Regeneration", healthAmount);
            Destroy(gameObject);
        }
    }
}
