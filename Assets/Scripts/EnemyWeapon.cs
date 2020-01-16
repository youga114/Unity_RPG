using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EnemyController enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && enemy.currentState == EnemyController.State.Attack)
        {
            PlayerController player = col.GetComponent<PlayerController>();

            int attackPower = enemy.myParams.GetRandomAttack();
            player.myParams.SetEnemyAttack(attackPower);
        }
    }
}
