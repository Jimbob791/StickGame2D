using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject skyShard;
    [SerializeField] private GameObject skyStrike;
    [SerializeField] private GameObject skySlam;

    private float multi;
    private List<GameObject> shards;

    public void SkyShardAttack()
    {
        StartCoroutine(SpawnSkyShards());
    }

    public void SkystrikeAttack()
    {
        StartCoroutine(SpawnSkystrike());
    }

    public void SkySlamAttack()
    {
        Vector2 castPos = transform.position;
        GameObject slam = GameObject.Instantiate(skySlam, new Vector3(castPos.x, castPos.y - 0.8125f, 0f), Quaternion.identity);
        Destroy(slam, 1f);
    }

    private IEnumerator SpawnSkyShards()
    {
        Vector2 castPos = transform.position;
        float facing = player.GetComponent<Move>().facing;
        multi = GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;

        yield return new WaitForSeconds(1f / multi);

        shards = new List<GameObject>();

        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.6666666f / multi);
            GameObject shard = GameObject.Instantiate(skyShard, new Vector3(castPos.x + (facing * 5) + (i * facing * 5), castPos.y + 20, 0f), Quaternion.identity);
            shards.Add(shard);
        }

        yield return new WaitForSeconds(1f / multi);

        foreach(GameObject shard in shards)
        {
            shard.gameObject.GetComponent<SkyShard>().Rupture();
        }
    }

    private IEnumerator SpawnSkystrike()
    {
        Vector2 castPos = transform.position;
        float facing = player.GetComponent<Move>().facing;
        multi = GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;

        yield return new WaitForSeconds(0.6666666f / multi);

        GameObject strike = GameObject.Instantiate(skyStrike, new Vector3(castPos.x + (facing * 7), castPos.y, 0f), Quaternion.identity);
    }
}