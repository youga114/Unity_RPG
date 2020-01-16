using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 180.0f;

    [System.NonSerialized]
    public int money = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCoinValue(int money)
    {
        this.money = money;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerParams>().AddMoney(money);

            //Destroy(gameObject);
            RemoveFromWorld();
        }
    }

    public void RemoveFromWorld()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
    }
}
