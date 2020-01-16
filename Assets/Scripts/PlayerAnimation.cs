using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // 애니메이터 컨트롤러의 전이 관계에서 설정한 번호에 맞춥니다.
    public const int ANI_IDLE = 0;
    public const int ANI_RUN = 1;
    public const int ANI_ATTACK = 2;
    public const int ANI_DIE = 3;
    
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
