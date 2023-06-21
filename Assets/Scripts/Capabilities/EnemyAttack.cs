using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D hitCollider;
    [SerializeField] private GameObject HitEffectPrefab;
    [SerializeField] private float duration;

    private List<Collider2D> collidersDamaged;
    private float elapsed;

    void Start()
    {
        collidersDamaged = new List<Collider2D>();
    }

    void Update()
    {
        if (animator.GetFloat("WeaponActive") > 0f)
        {
            Debug.Log("Check"); 
            Collider2D[] collidersToDamage = new Collider2D[10];
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = true;
            int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
            for (int i = 0; i < colliderCount; i++)
            {

                if (!collidersDamaged.Contains(collidersToDamage[i]))
                {
                    TeamComponent hitTeamComponent = collidersToDamage[i].gameObject.GetComponent<TeamComponent>();

                    // Only check colliders with a valid Team Componnent attached
                    if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Player)
                    {
                        GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].gameObject.transform.position, Quaternion.identity);
                        Debug.Log("Player Has Taken Damage");
                        collidersDamaged.Add(collidersToDamage[i]);
                    }
                }
            }
        }
        else
        {
            collidersDamaged = new List<Collider2D>();
        }
    }
}
