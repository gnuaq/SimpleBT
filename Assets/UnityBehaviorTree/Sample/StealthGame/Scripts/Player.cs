﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 InitPosition;

    private void Start()
    {
        InitPosition = transform.position;
    }

    public void CollectGold(Gold gold)
    {
        StealthGameManager.Instance.Golds.ToList().ForEach(g =>
        {
            if (g.Equals(gold))
            {
                StealthGameManager.Instance.Golds.Remove(g);
                Destroy(gold.gameObject);
            }
        });
        StealthGameManager.Instance.CollectGold();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var gold = other.gameObject.GetComponent<Gold>();
        if (gold != null)
        {
            CollectGold(gold);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var goard = other.gameObject.GetComponent<Guard>();
        if (goard != null)
        {
            StealthGameManager.Instance.IsEndGame = true;
            StealthGameManager.Instance.ShowEndGameUI();
        }
    }
}
