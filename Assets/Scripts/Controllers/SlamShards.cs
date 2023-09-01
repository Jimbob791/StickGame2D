using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlamShards : MonoBehaviour
{
    [SerializeField] private int slamDamage;
    [SerializeField] private float stunTime;

    [SerializeField] private Vector2 knockback;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D slamHitbox;
    [SerializeField] private GameObject HitEffectPrefab;
    [SerializeField] private CinemachineImpulseSource ruptureSource;

    private int damageToDeal;
    private string attackName = "slamShards";
    private Collider2D hitCollider;
    private List<Collider2D> collidersDamaged = new List<Collider2D>();

    void Start()
    {
        float multi = GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        animator.speed = 0.5f * multi;
        damageToDeal = slamDamage;
        hitCollider = slamHitbox;
        CheckHit();
        CameraShakeManager.current.CameraShake(ruptureSource);
        GameObject.Find("LandSound").GetComponent<AudioSource>().Play();
    }

    private void CheckHit()
    {
        Collider2D[] collidersToDamage = new Collider2D[50];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {
            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].gameObject.transform.position, Quaternion.identity);
                    collidersDamaged.Add(collidersToDamage[i]);
                    collidersToDamage[i].gameObject.GetComponent<EnemyMove>().Hit(-1 * damageToDeal, knockback, stunTime, this.gameObject, attackName);
                }
            }
        }
    }
}
