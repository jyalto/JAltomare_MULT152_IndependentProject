using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText = default;

    private void OnEnable()
    {
        PlayerMovement.OnDamage += UpdateHealth;
        PlayerMovement.OnHeal += UpdateHealth;
    }
    private void OnDisable()
    {
        PlayerMovement.OnDamage -= UpdateHealth;
        PlayerMovement.OnHeal -= UpdateHealth;
    }

    private void Start()
    {
        UpdateHealth(100);
    }
    private void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString("00");
    }
}
