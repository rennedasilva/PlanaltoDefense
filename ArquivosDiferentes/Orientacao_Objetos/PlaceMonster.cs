using System;
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
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool CanPlaceMonster()
    {        
        return (Monster == null && GameManager.Gold >= 200);
    }

    
    void OnMouseUp()
    {        
        if (CanPlaceMonster())        
            AddNewTower();        
        else if (CanUpgradeMonster())        
            UpgradeTower();        
    }

    private void UpgradeTower()
    {
        Monster.GetComponent<MonsterData>().IncreaseLevel();
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);

        GameManager.Gold -= Monster.GetComponent<MonsterData>().CurrentLevel.cost;
    }

    private void AddNewTower()
    {
        Monster = (GameObject)Instantiate(MonsterPrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);

        GameManager.Gold -= Monster.GetComponent<MonsterData>().CurrentLevel.cost;
    }

    private bool CanUpgradeMonster()
    {
        if (Monster != null)
        {
            MonsterData monsterData = Monster.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.GetNextLevel();
            if (nextLevel != null)            
                return GameManager.Gold >= nextLevel.cost;            
        }
        return false;
    }

}
