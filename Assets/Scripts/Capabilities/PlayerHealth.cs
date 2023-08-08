using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 20)] public int maxHealth;
    [SerializeField] private HealthBarManager barManager;

    private int currentHealth;
    private List<Coroutine> routines = new List<Coroutine>();
    private bool invincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        barManager.UpdateBar(currentHealth, maxHealth);
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
