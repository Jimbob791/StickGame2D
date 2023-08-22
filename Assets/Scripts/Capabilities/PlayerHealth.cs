using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 20)] public int maxHealth;
    [Range(0, 200)] public int maxMana;
    [SerializeField] private HealthBarManager barManager;
    [SerializeField] private Slider manaSlider;

    public int currentHealth;
    public int currentMana;
    private List<Coroutine> routines = new List<Coroutine>();
    private bool invincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        barManager.UpdateBar(currentHealth, maxHealth);
    }

    void Update()
    {

    }

    public void ChangeHealth(int amount)
    {
        if (invincible)
        {
            return;
        }

        foreach (Coroutine r in routines)
        {
            StopCoroutine(r);
        }

        Coroutine nr = StartCoroutine(IFrames());
        routines.Add(nr);

        currentHealth += amount;
        barManager.UpdateBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            // death
        }
    }

    IEnumerator IFrames()
    {
        invincible = true;
        yield return new WaitForSeconds(2f);
        invincible = false;
    }
}
