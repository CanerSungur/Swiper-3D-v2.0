﻿using System;
using UnityEngine;

public class Grass : MonoBehaviour, ITile
{
    public GameObject Player { get; set; }
    public int SpawnChance { get { return spawnChance; } set { spawnChance = value; } }
    [SerializeField] private int spawnChance;
    private void Awake()
    {
        Player = GameObject.Find("Player");
    }
    private void Update()
    {
        DestroySelf();
    }

    public void DestroySelf()
    {
        if (Player.transform.position.z - transform.position.z >= 20) Destroy(gameObject);
    }
}
