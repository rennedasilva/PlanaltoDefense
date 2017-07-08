﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab;

    public GameObject MonsterPrefab
    {
        get
        {
            try
            {
                return monsterPrefab;
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
                monsterPrefab = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private GameObject monster;

    private GameObject Monster
    {
        get
        {
            try
            {
                return monster;
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
                monster = value;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool canPlaceMonster()
    {
        //MonsterData monsterData = monster.GetComponent<MonsterData>();
        return (monster == null && gameManager.Gold >= 200);
    }

    //1
    void OnMouseUp()
    {
        //2
        if (canPlaceMonster())
        {
            //3
            monster = (GameObject)
                Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            //4
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;

        }
        else if (canUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().increaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;

        }
    }

    private bool canUpgradeMonster()
    {
        if (monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.getNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

}
