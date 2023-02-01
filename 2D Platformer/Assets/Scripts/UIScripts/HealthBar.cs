using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Health playerHealth;

    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text healthText;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
            playerHealth = player.GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateHealthPercentage(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        healthText.text = "HP : " + (healthSlider.value * 100).ToString();
    }

    private void OnEnable()
    {
        playerHealth.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerHealth.healthChanged.RemoveListener(OnPlayerHealthChanged);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float CalculateHealthPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    void OnPlayerHealthChanged(float currentHealth, float maxHealth)
    {
        healthSlider.value = CalculateHealthPercentage(currentHealth, maxHealth);
        healthText.text = "HP : " + (healthSlider.value * 100).ToString();
    }
}
