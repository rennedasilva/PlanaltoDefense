using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionDelegate : MonoBehaviour {

    public delegate void EnemyDelegate(GameObject enemy);

    public EnemyDelegate enemyDelegate;

    public EnemyDelegate EnemyDelegateProp
    {
        get
        {
            try
            {
                return enemyDelegate;
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
                enemyDelegate = value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        if (enemyDelegate != null)
        {
            enemyDelegate(gameObject);
        }
    }

}
