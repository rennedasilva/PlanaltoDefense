using System;
using System.Collections;
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

    public List<MonsterLevel> Levels {
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
        CurrentLevel = levels[0];
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
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }

    public MonsterLevel getNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void increaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }

}
