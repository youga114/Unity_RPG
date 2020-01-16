using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public GameObject coinPrefab;
    public int initialCoins = 30;

    List<GameObject> coins = new List<GameObject>();
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

        MakeCoins();
    }

    void MakeCoins()
    {
        for(int i=0;i<initialCoins;i++)
        {
            GameObject tempCoin = Instantiate(coinPrefab) as GameObject;

            //새로 생성된 코인들이 오브젝트 매니저의 자식 오브젝트로 들어감, 하이라키 뷰에서 관리가 수월함
            tempCoin.transform.parent = transform;

            tempCoin.SetActive(false);
            coins.Add(tempCoin);
        }
    }

    public void DropCoinToPosition(Vector3 pos, int coinValue)
    {
        GameObject reusedCoin = null;
        for(int i=0;i<coins.Count;i++)
        {
            if(coins[i].activeSelf == false)
            {
                reusedCoin = coins[i];
                break;
            }
        }
        if(reusedCoin == null)
        {
            GameObject newCoin = Instantiate(coinPrefab) as GameObject;
            coins.Add(newCoin);
            reusedCoin = newCoin;
        }
        reusedCoin.SetActive(true);
        reusedCoin.GetComponent<Coin>().SetCoinValue(coinValue);
        reusedCoin.transform.position = new Vector3(pos.x, reusedCoin.transform.position.y, pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
