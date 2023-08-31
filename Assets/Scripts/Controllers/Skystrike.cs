using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Skystrike : MonoBehaviour
{
    [SerializeField] private int pointDamage;
    [SerializeField] private int explosionDamage;
    [SerializeField] private float stunTime;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [SerializeField] private Vector2 knockback;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D pointHitbox;
    [SerializeField] private Collider2D explosionHitbox;
    [SerializeField] private GameObject HitEffectPrefab;

    private int damageToDeal;
    private string attackName = "skyStrike";
    private Collider2D hitCollider;
    private List<Collider2D> collidersDamaged = new List<Collider2D>();

    void Start()
    {
        Destroy(gameObject, 10f);
        StartCoroutine(StrikeSound());
    }

    void Update()
    {
        float multi = GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        animator.speed = 0.5f * multi;

        if (animator.GetFloat("Hit") > 0)
        {
            damageToDeal = pointDamage;
            hitCollider = pointHitbox;
            CheckHit();
            damageToDeal = explosionDamage;
            hitCollider = explosionHitbox;
            CheckHit();
        }
    }

    private void CheckHit()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);   
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // Only check colliders with a valid Team Componnent attached
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].gameObject.transform.position, Quaternion.identity);
                    collidersDamaged.Add(collidersToDamage[i]);
                    collidersToDamage[i].gameObject.GetComponent<EnemyMove>().Hit(-1 * damageToDeal, knockback, stunTime, this.gameObject, attackName);
                }
            }
        }
    }

    IEnumerator StrikeSound()
    {
        GameObject.Find("StrikeCharge").GetComponent<AudioSource>().Play();

        float multi = GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        yield return new WaitForSeconds(2f / multi);

        GameObject.Find("StrikeHit").GetComponent<AudioSource>().Play();
        CameraShakeManager.current.CameraShake(impulseSource);
    }
}
