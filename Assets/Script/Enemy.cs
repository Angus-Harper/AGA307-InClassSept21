using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : GameBehaviour
{
    public EnemyType myType;
    public PatrolType patrolType;
    int patrolPoint = 0;
    bool reverse = false;
   // public EnemyMananger _EM; // _EM has been cancled due to GameBehaviour ^
    float moveDistance = 500;
    public Transform moveToPos;
    float baseSpeed = 2;
    float baseHealth = 50;
    Transform startPos;
    float mySpeed;
    public float myHealth;

    Animator anim;
    NavMeshAgent agent;
    int currentWaypoint;
    float detectDistance = 10;
    public float detectTime = 5;
    void Start()
    {
        Setup();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        SetNav();
    }

    void SetNav()
    {
        currentWaypoint = Random.Range(0, _EM.spawnPoint.Length);
        agent.SetDestination(_EM.spawnPoint[currentWaypoint].position);
        ChangeSpeed(mySpeed);
    }

    void ChangeSpeed(float _speed)
    {
        agent.speed = _speed;
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, _P.transform.position);

        if (distToPlayer <=detectDistance)
        {
            if (patrolType != PatrolType.Chase)
            {
                patrolType = PatrolType.Detect;
            }
        }

        switch(patrolType)
        {
            case PatrolType.Chase:
                agent.SetDestination(_P.transform.position);
                ChangeSpeed(mySpeed * 2);
                if (distToPlayer > detectDistance)
                    patrolType = PatrolType.Detect;
                break;
            case PatrolType.Detect:
                agent.SetDestination(transform.position);
                ChangeSpeed(0);
                detectTime -= Time.deltaTime;
                if (distToPlayer <= detectDistance)
                {
                    patrolType = PatrolType.Chase;
                    detectTime = 5;
                }
                if (detectTime <= 0)
                {
                    patrolType = PatrolType.Patrol;
                    SetNav();
                }
                break;
            case PatrolType.Patrol:
                float distToWaypoint = Vector3.Distance(transform.position, _EM.spawnPoint[currentWaypoint].position);
                if (distToWaypoint < 1)
                     SetNav();
                detectTime = 5;
                break;
        }
        anim.SetFloat("Speed", agent.speed);
    }

    void Setup()
    {
        switch(myType)
        {
            case EnemyType.OneHanded:
                mySpeed = baseSpeed;
                myHealth = baseHealth;
                break;
            case EnemyType.TwoHanded:
                mySpeed = baseSpeed / 2f;
                myHealth = baseHealth * 2f;
                break;
            case EnemyType.Archer:
                mySpeed = baseSpeed * 2f;
                myHealth = baseHealth / 2f;
                break;
        }
    }

    public void Hit(int _damage)
    {
        myHealth -= 20;
        if (myHealth <= 0)
            Die();
        else
        {
            GameEvent.ReportEnemyHit(gameObject);
            int rnd = Random.Range(1, 4);
            anim.SetTrigger("Hit1");

            if (rnd == 1)
            {
                anim.SetTrigger("Hit1");
            }
            if (rnd == 2)
            {
                anim.SetTrigger("Hit2");
            }
            if (rnd == 3)
            {
                anim.SetTrigger("Hit3");
            }
        }
            
    }
    void Die()
    {
       // GameEvent.ReportEnemyDied(gameObject);
        int rnd = Random.Range(1, 4);
        if (rnd == 1)
        {
            anim.SetTrigger("Die1");
        }
        if (rnd == 2)
        {
            anim.SetTrigger("Die2");
        }
        if (rnd == 3)
        {
            anim.SetTrigger("Die3");
        }
    }

    int RandomAnimation()
    {
        return Random.Range(1, 4);
    }
}
