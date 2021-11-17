using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float myHealth;
    
    void Start()
    {
        Setup();
        startPos = transform;
        //_EM = FindObjectOfType<EnemyMananger>();
        moveToPos = _EM.spawnPoint[Random.Range(0, _EM.spawnPoint.Length)];
        StartCoroutine(Move());
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            Hit(20);
    }

    void Hit(int _damage)
    {
        myHealth -= _damage;
        if (myHealth <= 0)
            Die();
        else
            GameEvent.ReportEnemyHit(gameObject);
    }
    void Die()
    {
        GameEvent.ReportEnemyDied(gameObject);
    }

    IEnumerator Move()
    {
        Transform _newPos = transform;

        switch(patrolType)
        {
            case PatrolType.Random:
                _newPos = _EM.spawnPoint[Random.Range(0, _EM.spawnPoint.Length)];
                break;
            case PatrolType.Linear:
                _newPos = _EM.spawnPoint[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoint.Length ? patrolPoint + 1 : 0;
                break;
            case PatrolType.Repeat:
                _newPos = reverse ? startPos : moveToPos;
                reverse = !reverse;
                break;
        }
        /*for(int i = 0; i < moveDistance; i++)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
            yield return null;
        }*/

        while (Vector3.Distance(transform.position, _newPos.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _newPos.position, Time.deltaTime * mySpeed);
            transform.rotation = Quaternion.LookRotation(_newPos.position); // rotation
            yield return null;
        }
        yield return new WaitForSeconds(2);

        StartCoroutine(Move());
    }
}
