using System;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public float speed;
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
    public BulletBehavior()
    {
        Speed = 10;
    }
    // Use this for initialization
    void Start()
    {
        StartTime = Time.time;
        Distance = Vector3.Distance(StartPosition, TargetPosition);
        GameObject gm = GameObject.Find("GameManager");
        GameManager = gm.GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1 
        float timeInterval = Time.time - StartTime;
        gameObject.transform.position = Vector3.Lerp(StartPosition, TargetPosition, timeInterval * Speed / Distance);

        // 2 
        if (gameObject.transform.position.Equals(TargetPosition))
        {
            if (Target != null)
            {
                // 3
                Transform healthBarTransform = Target.transform.Find("HealthBar");
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.CurrentHealth -= Mathf.Max(Damage, 0);
                // 4
                if (healthBar.CurrentHealth <= 0)
                {
                    Destroy(Target);
                    AudioSource audioSource = Target.GetComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                    GameManager.Gold += 50;
                }
            }
            Destroy(gameObject);
        }
    }
}
