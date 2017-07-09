using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{

    public Text goldLabel;

    public Text GoldLabel
    {
        get
        {
            try
            {
                return goldLabel;
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
                goldLabel = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private int gold;

    public Text waveLabel;

    public Text WaveLabel
    {
        get
        {
            try
            {
                return waveLabel;
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
                waveLabel = value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public GameObject[] nextWaveLabels;

    public GameObject[] NextWaveLabels
    {
        get
        {
            try
            {
                return nextWaveLabels;
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
                nextWaveLabels = value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public Text healthLabel;

    public Text HealthLabel
    {
        get
        {
            try
            {
                return healthLabel;
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
                healthLabel = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public GameObject[] healthIndicator;

    public GameObject[] HealthIndicator
    {
        get
        {
            try
            {
                return healthIndicator;
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
                healthIndicator = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public bool gameOver;

    public bool GameOver
    {
        get
        {
            try
            {
                return gameOver;
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
                gameOver = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private int wave;
    private int health;

    public GameManagerBehavior()
    {
        GameOver = false;
    }

    void Start()
    {
        Wave = 0;
        Gold = 500;
        Health = 5;
    }

    void Update() { }

    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            GoldLabel.GetComponent<Text>().text = string.Format("Seus pontos: {0}", gold);
        }
    }

    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (!GameOver)
                for (int i = 0; i < NextWaveLabels.Length; i++)
                    NextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");

            WaveLabel.text = string.Format("Fase: {0}", (wave + 1));
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            if (value < health)
                Camera.main.GetComponent<CameraShake>().Shake();

            health = value;
            HealthLabel.text = string.Format("Propina: R${0} bi", health);

            if (health <= 0 && !GameOver)
            {
                PerformGameOver();
            }

            UpdateHealthprite();
        }
    }

    private void UpdateHealthprite()
    {
        for (int i = 0; i < HealthIndicator.Length; i++)
            HealthIndicator[i].SetActive(i < Health);
    }

    private void PerformGameOver()
    {
        GameOver = true;
        GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
        gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
    }
}
