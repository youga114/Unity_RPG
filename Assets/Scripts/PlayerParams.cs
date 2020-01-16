using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParams : CharacterParams
{
    public string name { get; set; }
    public int curExp { get; set; }
    public int expToNextLevel { get; set; }
    public int money { get; set; }

    public override void InitParams()
    {
        name = "Jun";
        level = 1;
        maxHp = 100;
        curHp = 70;
        attackMin = 5;
        attackMax = 8;
        defense = 1;

        curExp = 0;
        expToNextLevel = 100 * level;
        money = 0;

        isDead = false;

        UIManager.instance.UpdatePlayerUI(this);
    }

    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();

        UIManager.instance.UpdatePlayerUI(this);
    }

    public void AddMoney(int money)
    {
        this.money += money;
    }
}
