using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterLevel
{
    public int cost;

    public int Cost
    {
        get
        {
            try
            {
                return cost;
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
                cost = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public GameObject visualization;

    public GameObject Visualization
    {
        get
        {
            try
            {
                return visualization;
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
                visualization = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public GameObject bullet;

    public GameObject Bullet
    {
        get
        {
            try
            {
                return bullet;
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
                bullet = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public float fireRate;

    public float FireRate
    {
        get
        {
            try
            {
                return fireRate;
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
                fireRate = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels;

    public List<MonsterLevel> Levels
    {
        get
        {
            try
            {
                return levels;
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
                levels = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private MonsterLevel currentLevel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        CurrentLevel = Levels[0];
    }

    //1
    public MonsterLevel CurrentLevel
    {
        //2
        get
        {
            return currentLevel;
        }
        //3
        set
        {
            currentLevel = value;
            int currentLevelIndex = Levels.IndexOf(currentLevel);

            GameObject levelVisualization = Levels[currentLevelIndex].visualization;
            for (int i = 0; i < Levels.Count; i++)
                if (levelVisualization != null)
                    Levels[i].visualization.SetActive(i == currentLevelIndex);
        }
    }

    public MonsterLevel getNextLevel()
    {
        int currentLevelIndex = Levels.IndexOf(CurrentLevel);
        int maxLevelIndex = Levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return Levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void increaseLevel()
    {
        int currentLevelIndex = Levels.IndexOf(currentLevel);
        if (currentLevelIndex < Levels.Count - 1)
        {
            CurrentLevel = Levels[currentLevelIndex + 1];
        }
    }

}
