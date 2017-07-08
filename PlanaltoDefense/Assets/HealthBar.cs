using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public float maxHealth; // = 100;
 
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

    public float currentHealth; // = 100;

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

    // Use this for initialization
    void Start () {
        OriginalScale = gameObject.transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = CurrentHealth / MaxHealth * OriginalScale;
        gameObject.transform.localScale = tmpScale;
    }
}
