using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    void Update()
    {
        if (player.transform.position.y < 0)
        {
            Debug.Log("GAME OVER.");
        }
    }
}
