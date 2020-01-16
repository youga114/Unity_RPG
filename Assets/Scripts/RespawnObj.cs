using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObj : MonoBehaviour
{
    List<Transform> spawnPos = new List<Transform>();
    GameObject[] monsters;

    public GameObject monPrefab;
    public int spawnNumber = 1;
    public float respawnDelay = 3.0f;

    int deadMosters = 0;

    // Start is called before the first frame update
    void Start()
    {
        MakeSpawnPos();
    }

    void MakeSpawnPos()
    {
        foreach(Transform pos in transform)
        {
            if(pos.tag == "Respawn")
            {
                spawnPos.Add(pos);
            }
        }
        if(spawnNumber>spawnPos.Count)
        {
            spawnNumber = spawnPos.Count;
        }

        monsters = new GameObject[spawnNumber];

        MakeMonsters();
    }

    void MakeMonsters()
    {
        for(int i=0;i<spawnNumber;i++)
        {
            GameObject mon = Instantiate(monPrefab, spawnPos[i].position, Quaternion.identity) as GameObject;

            mon.GetComponent<EnemyController>().SetRespawnObj(gameObject, i, spawnPos[i].position);

            mon.SetActive(false);

            monsters[i] = mon;

            GameManager.instance.AddNewMonsters(mon);
        }
    }

    public void RemoveMonster(int spawnID)
    {
        deadMosters++;

        monsters[spawnID].SetActive(false);
        print(spawnID + "monster was killed");

        //죽은 몬스터의 숫자가 몬스터 배열의 크기와 같다면 일정시간 후에 몬스터를 다시 생성
        if(deadMosters == monsters.Length)
        {
            StartCoroutine(InitMonsters());
            deadMosters = 0;
        }
    }

    IEnumerator InitMonsters()
    {
        yield return new WaitForSeconds(respawnDelay);

        GetComponent<SphereCollider>().enabled = true;
    }

    void SpawnMonster()
    {
        for(int i=0;i<monsters.Length;i++)
        {
            monsters[i].GetComponent<EnemyController>().AddToWorldAgain();
            monsters[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            SpawnMonster();
            GetComponent<SphereCollider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
