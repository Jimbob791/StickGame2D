using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Range(0, 200)] public int maxHealth;
    [SerializeField] private Slider slider;

    private int currentHealth;
    private List<Coroutine> routines = new List<Coroutine>();

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        foreach (Coroutine r in routines)
        {
            StopCoroutine(r);
        }

        slider.GetComponent<Animator>().SetBool("FadeIn", true);
        Coroutine nr = StartCoroutine(AnimWait());
        routines.Add(nr);
          
        currentHealth += amount;

        slider.maxValue = maxHealth;
        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            // death
        }
    }

    IEnumerator AnimWait()
    {
        yield return new WaitForSeconds(2f);
        slider.GetComponent<Animator>().SetBool("FadeIn", false);
    }
}
