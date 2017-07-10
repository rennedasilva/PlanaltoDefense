using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public float maxHealth;

    public float MaxHealth
    {
        get
        {
            try
            {
                return maxHealth;
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
                maxHealth = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public float currentHealth;

    public float CurrentHealth
    {
        get
        {
            try
            {
                return currentHealth;
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
                currentHealth = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private float originalScale;

    private float OriginalScale
    {
        get
        {
            try
            {
                return originalScale;
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
                originalScale = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public HealthBar()
    {
        MaxHealth = 100;
        CurrentHealth = 100;
    }

    void Start()
    {
        OriginalScale = gameObject.transform.localScale.x;
    }

    void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = CurrentHealth / MaxHealth * OriginalScale;
        gameObject.transform.localScale = tmpScale;
    }
}
