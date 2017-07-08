using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

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

    public float speed; // = 1.0f;
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

    // Use this for initialization
    void Start () {
		LastWaypointSwitchTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        // 1 
        Vector3 startPosition = Waypoints[CurrentWaypoint].transform.position;
        Vector3 endPosition = Waypoints[CurrentWaypoint + 1].transform.position;
        // 2 
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / Speed;
        float currentTimeOnPath = Time.time - LastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        // 3 
        if (gameObject.transform.position.Equals(endPosition))
        {
            if (CurrentWaypoint < Waypoints.Length - 2)
            {
                // 3.a 
                CurrentWaypoint++;
                LastWaypointSwitchTime = Time.time;
                // RotateIntoMoveDirection(); // não gira mais o inimigo
            }
            else
            {
                // 3.b 
                Destroy(gameObject);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                // deduct health
                GameManagerBehavior gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
                gameManager.Health -= 1;
            }
        }
    }

    private void RotateIntoMoveDirection()
    {
        //1
        Vector3 newStartPosition = Waypoints[CurrentWaypoint].transform.position;
        Vector3 newEndPosition = Waypoints[CurrentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);
        //2
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        //3
        GameObject sprite = (GameObject)
            gameObject.transform.Find("Sprite").gameObject;
        sprite.transform.rotation =
            Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }

    public float distanceToGoal()
    {
        float distance = 0;
        distance += Vector3.Distance(
            gameObject.transform.position,
            Waypoints[CurrentWaypoint + 1].transform.position);
        for (int i = CurrentWaypoint + 1; i < Waypoints.Length - 1; i++)
        {
            Vector3 startPosition = Waypoints[i].transform.position;
            Vector3 endPosition = Waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }

}
