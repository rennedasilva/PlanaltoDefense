using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;

    public GameObject EnemyPrefab
    {
        get
        {
            try
            {
                return enemyPrefab;
            }
            catch (Exception)
            {
                throw;
            }
        }
        set
        {
            try
            {
                enemyPrefab = EnemyPrefab;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public float spawnInterval;// = 2;

    public float SpawnInterval
    {
        get
        {
            try
            {
                return spawnInterval;
            }
            catch (Exception)
            {

                throw;
            }
        }
        set
        {
            try
            {
                spawnInterval = value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public int maxEnemies;// = 20;

    public int MaxEnemies
    {
        get
        {
            try
            {
                return maxEnemies;
            }
            catch (Exception)
            {
                throw;
            }
        }
        set
        {
            try
            {
                maxEnemies = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public Wave()
    {
        SpawnInterval = 2;
        MaxEnemies = 20;
    }
}

public class SpawnEnemy : MonoBehaviour
{

    public GameObject[] waypoints;
    public Wave[] waves;

    public Wave[] Waves
    {
        get
        {
            try
            {
                return waves;
            }
            catch (Exception)
            {
                throw;
            }
        }
        set
        {
            try
            {
                waves = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    // Use this for initialization
    void Start()
    {
        // Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1
        int currentWave = gameManager.Wave;
        if (currentWave < Waves.Length)
        {
            // 2
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = Waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < Waves[currentWave].maxEnemies)
            {
                // 3  
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)Instantiate(Waves[currentWave].enemyPrefab);
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            // 4 
            if (enemiesSpawned == Waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            // 5 
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }

    }
}
