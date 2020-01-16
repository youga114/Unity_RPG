using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5.0f;
    float rotateSpeed = 360.0f;

    float attackDelay = 0.5f;
    float attackTimer = 0.0f;

    float h;
    float v;

    public enum State
    {
        Idle,
        Move,
        Attack,
        Dead
    }

    //idle 상태를 기본 상태로 지정
    public State currentState = State.Idle;

    PlayerAnimation myAni;

    public PlayerParams myParams;

    // Start is called before the first frame update
    void Start()
    {
        myAni = GetComponent<PlayerAnimation>();
        myParams = GetComponent<PlayerParams>();

        myParams.InitParams();

        myParams.deadEvent.AddListener(ChangeToPlayerDead);

        ChangeState(State.Idle, PlayerAnimation.ANI_IDLE);
    }

    public void ChangeToPlayerDead()
    {
        print("player was dead");
        ChangeState(State.Dead, PlayerAnimation.ANI_DIE);

        UIManager.instance.ShowGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();       //입력 검사
        StateAct();         //상태별 행동
    }

    void CheckInput()
    {
        // 이동키 입력
        if(currentState==State.Idle || currentState==State.Move)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            if (h != 0.0f || v != 0.0f)
            {
                //상태,애니메이션 활성화(Run)
                ChangeState(State.Move, PlayerAnimation.ANI_RUN);
            }
            else
            {
                //상태,애니메이션 활성화(Idle)
                ChangeState(State.Idle, PlayerAnimation.ANI_IDLE);
            }
        }


        // 스페이스 입력(공격)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //상태,애니메이션 활성화(Attack)
            ChangeState(State.Attack, PlayerAnimation.ANI_ATTACK);
        }
    }

    void StateAct()
    {
        switch(currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Move:
                MoveState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Dead:
                DeadState();
                break;
        }
    }
    
    void ChangeState(State newState, int aniNumber)
    {
        if (currentState == newState)
            return;

        myAni.ChangeAni(aniNumber);
        currentState = newState;
    }

    void IdleState()
    {

    }

    void MoveState()
    {
        //물체의 회전

        //방법1(급 회전)
        //Vector3 lookDirection = new Vector3(h, 0, v);
        //transform.rotation = Quaternion.LookRotation(lookDirection);

        // 방법2(부드러운 회전)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(h, 0, v));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        
        //물체의 이동
        transform.Translate(Vector3.forward * speed * Mathf.Lerp(0, 1, Mathf.Abs(h) + Mathf.Abs(v)) * Time.deltaTime);

    }

    void AttackState()
    {
        attackTimer += Time.deltaTime;
        
        if(attackTimer>attackDelay)
        {
            ChangeState(State.Idle, PlayerAnimation.ANI_IDLE);
            attackTimer = 0.0f;
        }
    }

    void DeadState()
    {

    }
}
