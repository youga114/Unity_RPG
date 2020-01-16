using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    List<GameObject> monsters = new List<GameObject>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AddNewMonsters(GameObject mon)
    {
        bool sameExist = false;
        for(int i=0;i<monsters.Count;i++)
        {
            if(monsters[i]==mon)
            {
                sameExist = true;

                break;
            }
        }

        if(sameExist == false)
        {
            monsters.Add(mon);
        }
    }

    public void Remove(GameObject mon)
    {
        foreach(GameObject monster in monsters)
        {
            if(monster == mon)
            {
                monsters.Remove(monster);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
