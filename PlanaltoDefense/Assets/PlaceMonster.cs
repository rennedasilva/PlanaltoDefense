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

    Func<GameObject, int, bool> CanPlaceMonster = (GameObject monster, int gold) => (monster == null && gold >= 200);

    Func<GameObject, int, bool> CanUpgradeMonster = (GameObject monster, int gold) =>
   {
       if (monster != null)
       {
           MonsterData monsterData = monster.GetComponent<MonsterData>();
           MonsterLevel nextLevel = monsterData.GetNextLevel();
           if (nextLevel != null)
               return gold >= nextLevel.cost;
       }
       return false;
   };


    void OnMouseUp()
    {
        if (CanPlaceMonster(Monster, GameManager.Gold))
            GameManager.Gold = SpentMoney(GameManager.Gold, AddNewTower(this));
        else if (CanUpgradeMonster(Monster, GameManager.Gold))
            GameManager.Gold = SpentMoney(GameManager.Gold, UpgradeTower(this));
    }

    Func<PlaceMonster, int> UpgradeTower = (PlaceMonster placeMonster) =>
    {
        GameObject monster = placeMonster.Monster;
        MonsterData monsterData = monster.GetComponent<MonsterData>();
        monsterData.CurrentLevel = monsterData.IncreaseLevel(monsterData.Levels.IndexOf(monsterData.CurrentLevel), monsterData.Levels);
        AudioSource audioSource = placeMonster.gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);

        return placeMonster.Monster.GetComponent<MonsterData>().CurrentLevel.cost;
    };

    Func<PlaceMonster, int> AddNewTower = (PlaceMonster placeMonster) =>
       {
           placeMonster.Monster = Instantiate(placeMonster.MonsterPrefab, placeMonster.transform.position, Quaternion.identity);
           AudioSource audioSource = placeMonster.gameObject.GetComponent<AudioSource>();
           audioSource.PlayOneShot(audioSource.clip);

           return placeMonster.Monster.GetComponent<MonsterData>().CurrentLevel.cost;
       };

    Func<int, int, int> SpentMoney = (int currentGold, int buy) => currentGold - buy;

}
