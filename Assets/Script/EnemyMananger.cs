using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    OneHanded,
    TwoHanded,
    Archer
}

public enum PatrolType
{
    Patrol,
    Detect,
    Chase
}
public class EnemyMananger : Singleton<EnemyMananger>
{ 
    public Transform[] spawnPoint;
    public GameObject[] enemyTypes;
    public List<GameObject> enemies;
    public string[] playerNames;
    public GameObject Player;
    public float spawnDelay = 2f;
    public int maxEnemies = 3;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        print(playerNames[0]);
        print(playerNames[1]);
        print(playerNames[2]);
        print(playerNames[3]);

        print("Number of players: " + playerNames.Length);
        //SpawnEnemy();
        /*
        for (int i = 0; i < 101; i++)
        {
            print(i);
        }
        */
        StartCoroutine(SpawnWithDelay());
    }

    IEnumerator SpawnWithDelay()
    {
        int rndEnemy = Random.Range(0, enemyTypes.Length);
        int rndSpawn = Random.Range(0, spawnPoint.Length);
        GameObject enemy = Instantiate(enemyTypes[rndEnemy], spawnPoint[rndSpawn].position, spawnPoint[rndSpawn].rotation);
        enemies.Add(enemy);
        _UI.UpdateEnemyCount(enemies.Count);
        yield return new WaitForSeconds(spawnDelay);
        if (enemies.Count <= maxEnemies)
        {
            StartCoroutine(SpawnWithDelay()); 
        }
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject enemy1 = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnPoint[i].position, spawnPoint[i].rotation);
            enemies.Add(enemy1);
        }
        print(enemies.Count);
    }
    void KillAllEnemiesOfAllType(string _condition)
    {
        //Loop through our enemy list
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
            {
                KillEnemy(enemies[i]);
            }
        }

        //If any enemy meets the condition (check if the name contains the condition)

        //Destroy and remove that enemy
    }
    
    void KillEnemy(GameObject _enemy)
    {
        Destroy(_enemy);
        enemies.Remove(_enemy);
        print(enemies.Count);
        _UI.UpdateEnemyCount(enemies.Count);
    }
    
    void Despawn()
    {
        if (enemies.Count == 0)
            return;
        Destroy(enemies[0]);
        enemies.RemoveAt(0);
    }

    GameObject FindClosestEnemy()
    {
        GameObject temp = null;
        float closest = Mathf.Infinity;
        for (int i = 0; i < enemies.Count; i++)
        {
            float dist = Vector3.Distance(Player.transform.position, enemies[i].transform.position);
            if (dist < closest)
            {
                temp = enemies[i];
                closest = dist;
            }
        }
        return temp;
    }

    void GetRandomNumber()
    {
        float rnd = Random.Range(0f, 10f);
        print("Random float is: " + rnd);
        int rndint = Random.Range(0, 10);
        print("Random int is: " + rndint);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Despawn();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnEnemy();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            print(FindClosestEnemy().name);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetRandomNumber();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            KillAllEnemiesOfAllType("arch");
        }
    }
    private void OnEnable()
    {
        GameEvent.OnEnemyDied += KillEnemy;
    }
    private void OnDisable()
    {
        GameEvent.OnEnemyDied -= KillEnemy;
    }
}
