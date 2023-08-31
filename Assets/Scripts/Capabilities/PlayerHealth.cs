using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 20)] public int maxHealth;
    [Range(0, 200)] public float maxMana;
    [SerializeField] private HealthBarManager barManager;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private float manaGrowth;

    public int currentHealth;
    public float currentMana;
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
        currentMana += manaGrowth * Time.deltaTime;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaSlider.value = currentMana / maxMana;
    }

    public bool CheckMana(float cost)
    {
        if(currentMana >= cost)
        {
            currentMana -= cost;
            manaSlider.value = currentMana / maxMana;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeHealth(int amount, Vector3 knockback)
    {
        if (invincible)
        {
            return;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = knockback * gameObject.GetComponent<Move>().facing * -1;

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
            GameObject.Find("LoadManager").GetComponent<LoadManager>().isWin = false;
            GameObject.Find("LoadManager").GetComponent<LoadManager>().LevelComplete();
        }
    }

    IEnumerator IFrames()
    {
        invincible = true;
        yield return new WaitForSeconds(2f);
        invincible = false;
    }
}
