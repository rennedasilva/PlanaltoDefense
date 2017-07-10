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

    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnEnable()
    {
        CurrentLevel = Levels[0];
    }

    public MonsterLevel CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            SelectLevelSprite(Levels.IndexOf(currentLevel));
        }
    }

    private void SelectLevelSprite(int currentLevelIndex)
    {
        GameObject levelVisualization = Levels[currentLevelIndex].Visualization;
        for (int i = 0; i < Levels.Count; i++)
            if (levelVisualization != null)
                Levels[i].Visualization.SetActive(i == currentLevelIndex);
    }

    public MonsterLevel GetNextLevel()
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

    public Func<int, List<MonsterLevel>, MonsterLevel> IncreaseLevel = (int currentLevelIndex, List<MonsterLevel> levels) => currentLevelIndex < levels.Count - 1 ? levels[currentLevelIndex + 1] : levels[currentLevelIndex];
}
