using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerStats : MonoBehaviour
{
    public Image hpBar;

    public float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = health / 100f;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 || health == 0) Invoke(nameof(DestroyPlayer), 0.5f);

        

        Debug.Log(health);
    }
    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    private void healHP(int heal)
    {
        health += heal;

        health = Mathf.Clamp(health, 0f, 100f);
    }
}
