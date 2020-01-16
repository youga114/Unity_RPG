using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 언제 어디서나 쉽게 접근할 수 있도록 하기위해 만든 정적변수
    public static UIManager instance;

    public Text playerName;
    public Image playerHPBar;

    public Text gameOver;
    Animator animGameOver;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        animGameOver = gameOver.gameObject.GetComponent<Animator>();
        gameOver.enabled = false;
    }

    public void ShowGameOver()
    {
        gameOver.enabled = true;
        animGameOver.SetTrigger("show");
    }

    public void UpdatePlayerUI(PlayerParams playerParams)
    {
        playerName.text = playerParams.name;
        playerHPBar.rectTransform.localScale = new Vector3((float)playerParams.curHp / (float)playerParams.maxHp, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
