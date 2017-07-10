using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRange;

    public List<GameObject> EnemiesInRange
    {
        get
        {
            try
            {
                return enemiesInRange;
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
                enemiesInRange = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private float lastShotTime;

    private float LastShotTime
    {
        get
        {
            try
            {
                return lastShotTime;
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
                lastShotTime = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private MonsterData monsterData;

    private MonsterData MonsterData
    {
        get
        {
            try
            {
                return monsterData;
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
                monsterData = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        EnemiesInRange = new List<GameObject>();
        LastShotTime = Time.time;
        MonsterData = gameObject.GetComponentInChildren<MonsterData>();
    }

    void Update()
    {
        GameObject target = ChoseClosestEnemy(float.MaxValue);
        if (target != null)
        {
            if (Time.time - lastShotTime > MonsterData.CurrentLevel.FireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                LastShotTime = Time.time;
            }
            // 3
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI, new Vector3(0, 0, 1));
        }
    }

    private GameObject ChoseClosestEnemy(float minimalEnemyDistance)
    {
        GameObject target = null;

        foreach (GameObject enemy in EnemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }

        return target;
    }

    // 1
    void OnEnemyDestroy(GameObject enemy)
    {
        EnemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            EnemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.EnemyDelegateProp += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            EnemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.EnemyDelegateProp -= OnEnemyDestroy;
        }
    }

    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = MonsterData.CurrentLevel.Bullet;
        // 1 
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        // 2 
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        // 3 
        Animator animator = MonsterData.CurrentLevel.visualization.GetComponent<Animator>();        
        AudioSource audioSource = MonsterData.CurrentLevel.visualization.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }

}
