using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Range(0, 200)] public int maxHealth;
    [SerializeField] private Slider slider;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth == maxHealth)
        {
            // hide bar
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        slider.maxValue = maxHealth;
        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            // death
        }
    }
}
