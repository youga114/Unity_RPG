using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public PlayerController player;
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
        if (col.tag == "Enemy" && player.currentState==PlayerController.State.Attack)
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            enemy.ShowHitEffect();

            int attackPower = player.myParams.GetRandomAttack();
            enemy.myParams.SetEnemyAttack(attackPower);
        }
    }
}
