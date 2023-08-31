using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SkyShard : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private int landDamage;
    [SerializeField] private int ruptureDamage;
    [SerializeField] private float stunTime;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [SerializeField] private Vector2 knockback;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ruptureEffect;
    [SerializeField] private Collider2D landHitbox;
    [SerializeField] private Collider2D ruptureHitbox;
    [SerializeField] private GameObject HitEffectPrefab;

    private Rigidbody2D body;
    private string state;
    private string attackName = "skyShard";
    private int damageToDeal;
    private Collider2D hitCollider;
    private List<Collider2D> collidersDamaged = new List<Collider2D>();

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        state = "fall";
        hitCollider = landHitbox;
    }

    void Update()
    {
        if (state == "fall")
        {
            body.velocity = new Vector3(0f, -fallSpeed, 0f);
        }
        else if (state == "land")
        {
            state = "wait";
            damageToDeal = landDamage;
            CheckHit();
        }
        else if (state == "wait")
        {
            body.velocity = Vector3.zero;
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            CameraShakeManager.current.CameraShake(impulseSource);
            GameObject.Find("LandSound").GetComponent<AudioSource>().Play();
            state = "land";
            body.velocity = Vector3.zero;
        }
    }

    public void Rupture()
    {
        if (state == "wait")
        {
            GameObject.Find("ShatterSound").GetComponent<AudioSource>().Play();
            animator.SetTrigger("Rupture");
            hitCollider = ruptureHitbox;
            damageToDeal = ruptureDamage;
            CheckHit();
            CameraShakeManager.current.CameraShake(impulseSource);
            GameObject pfx = GameObject.Instantiate(ruptureEffect, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(pfx, 2f);
            Destroy(gameObject, 2f);
        }
    }
}
