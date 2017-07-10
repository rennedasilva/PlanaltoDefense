using System;
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
                enemyPrefab = value;
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

    public GameObject[] waypoints;

    public GameObject[] Waypoints
    {
        get
        {
            try
            {
                return waypoints;
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
                waypoints = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public int timeBetweenWaves;

    public int TimeBetweenWaves
    {
        get
        {
            try
            {
                return timeBetweenWaves;
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
                timeBetweenWaves = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private GameManagerBehavior gameManager;

    private GameManagerBehavior GameManager
    {
        get
        {
            try
            {
                return gameManager;
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
                gameManager = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private float lastSpawnTime;

    private float LastSpawnTime
    {
        get
        {
            try
            {
                return lastSpawnTime;
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
                lastSpawnTime = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private int enemiesSpawned;

    private int EnemiesSpawned
    {
        get
        {
            try
            {
                return enemiesSpawned;
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
                enemiesSpawned = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public SpawnEnemy()
    {
        EnemiesSpawned = 0;
        TimeBetweenWaves = 5;
    }

    // Use this for initialization
    void Start()
    {
        LastSpawnTime = Time.time;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        int currentWave = GameManager.Wave;
        if (currentWave < Waves.Length)
        {
            float timeInterval = Time.time - LastSpawnTime;
            float spawnInterval = Waves[currentWave].SpawnInterval;
            if (((EnemiesSpawned == 0 && timeInterval > TimeBetweenWaves) || timeInterval > spawnInterval) && EnemiesSpawned < Waves[currentWave].MaxEnemies)
                SpawnEnemies(currentWave);
            if (EnemiesSpawned == Waves[currentWave].MaxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
                StartNextWave();
        }
        else
            EndGame();

    }

    private void SpawnEnemies(int currentWave)
    {
        LastSpawnTime = Time.time;
        GameObject newEnemy = (GameObject)Instantiate(Waves[currentWave].EnemyPrefab);
        newEnemy.GetComponent<MoveEnemy>().waypoints = Waypoints;
        EnemiesSpawned++;
    }

    private void EndGame()
    {
        GameManager.GameOver = true;
        GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
        gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
    }

    private void StartNextWave()
    {
        GameManager.Wave++;
        GameManager.Gold = Mathf.RoundToInt(GameManager.Gold * 1.1f);
        EnemiesSpawned = 0;
        LastSpawnTime = Time.time;
    }
}
