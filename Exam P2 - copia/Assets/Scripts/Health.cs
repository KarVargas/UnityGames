using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    float hp, maxHP = 100f;
    public Image health;

    void Start() {
        hp = maxHP;   
    }

    public void Regeneration(float amount) {
        hp = Mathf.Clamp(hp + amount, 0f, maxHP);
        health.transform.localScale = new Vector2(hp / maxHP, 1);
        if (hp > maxHP) {
            hp = maxHP;
        }
    }

    public void TakeDamage(float amount) {
        hp = Mathf.Clamp(hp - amount, 0f, maxHP);
        health.transform.localScale = new Vector2(hp / maxHP, 1);
        if (hp <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.LoadLevel("Lose");
    }

    void Update() {
        //health.fillAmount = (hp / 100);
    }
}
