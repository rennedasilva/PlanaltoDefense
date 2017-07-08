using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public float speed = 10;
    public float Speed
    {
        get
        {
            try
            {
                return speed;
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
                speed = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public int damage;

    public int Damage
    {
        get
        {
            try
            {
                return damage;
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
                damage = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public GameObject target;

    public GameObject Target
    {
        get
        {
            try
            {
                return target;
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
                target = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public Vector3 startPosition;

    public Vector3 StartPosition
    {
        get
        {
            try
            {
                return startPosition;
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
                startPosition = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public Vector3 targetPosition;

    public Vector3 TargetPosition
    {
        get
        {
            try
            {
                return targetPosition;
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
                targetPosition = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private float distance;

    private float Distance
    {
        get
        {
            try
            {
                return distance;
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
                distance = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private float startTime;

    private float StartTime
    {
        get
        {
            try
            {
                return startTime;
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
                startTime = value;
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

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1 
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // 2 
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                // 3
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =
                    healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                // 4
                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    AudioSource audioSource = target.GetComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                    gameManager.Gold += 50;
                }
            }
            Destroy(gameObject);
        }
    }
}
