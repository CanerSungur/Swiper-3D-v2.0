using System;
using UnityEngine;

public class Grass : MonoBehaviour, ITile
{
    public GameObject player { get; set; }

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        DestroySelf();
    }

    public void DestroySelf()
    {
        if (player.transform.position.z - transform.position.z >= 20) Destroy(gameObject);
    }
}
