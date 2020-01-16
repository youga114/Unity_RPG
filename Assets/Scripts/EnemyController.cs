using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Attack,
        Dead,
        NoState
    }

    public State currentState = State.Idle;

    EnemyAnimation myAni;

    public EnemyParams myParams;

    Transform playerTransform;

    CharacterController controller;

    float chaseDistance = 5.0f;   //몬스터의 인식 거리
    float attackDistance = 1.5f;    //공격 거리
    float reChaseDistance = 2.0f;     //플레이어 재추적 거리

    float rotationSpeed = 360.0f;
    float speed = 1.3f;

    float attackDelay = 2.0f;
    float attackTimer = 0.0f;

    public ParticleSystem hitEffect;

    public GameObject selectMark;

    //리스폰 시킬 몬스터를 담을 변수
    GameObject myRespawnObj;

    //리스폰 오브젝트에서 생성된 몇번째 몬스터에 대한 정보
    public int spawnID { get; set; }

    //몬스터가 처음 생성될 때의 위치를 저장
    Vector3 originPos;
    
    // Start is called before the first frame update
    void Start()
    {
        myAni = GetComponent<EnemyAnimation>();
        myParams = GetComponent<EnemyParams>();
        myParams.deadEvent.AddListener(CallDeadEvent);
        controller = GetComponent<CharacterController>();

        ChangeState(State.Idle, EnemyAnimation.IDLE);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        hitEffect.Stop();
    }

    //몬스터가 어느 리스폰 오브젝트로부터 만들어졌는지에 대한 정보를 전달 받을 함수
    public void SetRespawnObj(GameObject respawnObj, int spawnID, Vector3 originPos)
    {
        myRespawnObj = respawnObj;
        this.spawnID = spawnID;
        this.originPos = originPos;
    }

    // Update is called once per frame
    void Update()
    {
        StateAct();   
    }


    void StateAct()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Chase:
                ChaseState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Dead:
                DeadState();
                break;
            case State.NoState:
                NoState();
                break;
        }
    }

    public void ChangeState(State newState, int aniNumber)
    {
        if(currentState==newState)
        {
            return;
        }

        currentState = newState;
        myAni.ChangeAni(aniNumber);
    }

    void IdleState()
    {
        if(GetDistanceFromPlayer() < chaseDistance)
        {
            ChangeState(State.Chase, EnemyAnimation.RUN);
        }
    }

    void ChaseState()
    {
        if(GetDistanceFromPlayer()<attackDistance)
        {
            ChangeState(State.Attack, EnemyAnimation.ATTACK);
        }
        else
        {
            TurnToDestination();
            MoveToDestination();
        }
    }

    void AttackState()
    {
        if(playerTransform.GetComponent<PlayerController>().currentState == PlayerController.State.Dead)
        {
            ChangeState(State.NoState, EnemyAnimation.IDLE);
        }

        if(GetDistanceFromPlayer()>reChaseDistance)
        {
            attackTimer = 0.0f;
            ChangeState(State.Chase, EnemyAnimation.RUN);
        }
        else
        {
            if(attackTimer > attackDelay)
            {
                transform.LookAt(playerTransform.position);
                myAni.ChangeAni(EnemyAnimation.ATTACK);

                attackTimer = 0.0f;
            }

            attackTimer += Time.deltaTime;
        }
    }

    void DeadState()
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void NoState()
    {

    }

    void TurnToDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(playerTransform.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    // 플레이어와 거리를 재는 함수
    float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        return distance;
    }
    
    public void ShowHitEffect()
    {
        hitEffect.Play();
    }

    void CallDeadEvent()
    {
        ChangeState(State.Dead, EnemyAnimation.DIE);

        //몬스터가 죽은 후 아이템 및 동전을 생성한다
        ObjectManager.instance.DropCoinToPosition(transform.position, myParams.rewardMoney);

        StartCoroutine(RemoveMeFromWorld());
    }

    IEnumerator RemoveMeFromWorld()
    {
        yield return new WaitForSeconds(1.0f);

        ChangeState(State.Idle, EnemyAnimation.IDLE);

        //리스폰 오브젝트에 자기 자신을 제거해 달라는 요청
        myRespawnObj.GetComponent<RespawnObj>().RemoveMonster(spawnID);
    }

    public void AddToWorldAgain()
    {
        transform.position = originPos;

        GetComponent<EnemyParams>().InitParams();
    }
}
