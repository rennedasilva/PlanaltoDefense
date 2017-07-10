using System;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    [HideInInspector]
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

    private int currentWaypoint; // = 0;

    private int CurrentWaypoint
    {
        get
        {
            try
            {
                return currentWaypoint;
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
                currentWaypoint = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private float lastWaypointSwitchTime;

    private float LastWaypointSwitchTime
    {
        get
        {
            try
            {
                return lastWaypointSwitchTime;
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
                lastWaypointSwitchTime = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

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

    public MoveEnemy()
    {
        CurrentWaypoint = 0;
        Speed = 1.0f;
    }

    void Start()
    {
        LastWaypointSwitchTime = Time.time;
    }

    void Update()
    {
        Vector3 startPosition = Waypoints[CurrentWaypoint].transform.position;
        Vector3 endPosition = Waypoints[CurrentWaypoint + 1].transform.position;
        gameObject.transform.position = GetActualPosition(startPosition, endPosition);

        if (gameObject.transform.position.Equals(endPosition))
        {
            if (CurrentWaypoint < Waypoints.Length - 2)
                ChangeDirection();
            else
            {
                Destroy(gameObject);
                EnemyPerformAtack(this);
            }
        }        
    }

    Func<MoveEnemy, bool> EnemyPerformAtack = (MoveEnemy moveEnemy) =>
    {
        

        AudioSource audioSource = moveEnemy.gameObject.GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(audioSource.clip, moveEnemy.transform.position);

        GameManagerBehavior gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        gameManager.Health -= 1;
        return true;
    };

    private void ChangeDirection()
    {
        CurrentWaypoint++;
        LastWaypointSwitchTime = Time.time;
    }

    private Vector3 GetActualPosition(Vector3 startPosition, Vector3 endPosition)
    {
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / Speed;
        float currentTimeOnPath = Time.time - LastWaypointSwitchTime;
        return Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector3.Distance(gameObject.transform.position, Waypoints[CurrentWaypoint + 1].transform.position);

        for (int i = CurrentWaypoint + 1; i < Waypoints.Length - 1; i++)
        {
            Vector3 startPosition = Waypoints[i].transform.position;
            Vector3 endPosition = Waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }

}
