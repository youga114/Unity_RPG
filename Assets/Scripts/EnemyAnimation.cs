using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public const int IDLE = 0;
    public const int RUN = 1;
    public const int ATTACK = 2;
    public const int DIE = 3;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //애니메이션 번호를 입력 받아서 플레이어의 애니메이션을 해당되는 애니메이션으로 바꿔주는 함수
    public void ChangeAni(int aniNumber)
    {
        animator.SetInteger("curState", aniNumber);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
