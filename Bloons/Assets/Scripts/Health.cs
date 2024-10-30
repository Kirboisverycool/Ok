using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float smoothValue;
    public int currentHealth;
    float currentVelocity = 0f;
    int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthText.text = "Health: " + currentHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Debug.Log("You Lose >:)");
            return;
        }

        float smoothSlider = Mathf.SmoothDamp(healthSlider.value, currentHealth, ref currentVelocity, smoothValue * Time.deltaTime);
        healthSlider.value = smoothSlider;
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;

        healthText.text = "Health " + currentHealth;
    }
}
