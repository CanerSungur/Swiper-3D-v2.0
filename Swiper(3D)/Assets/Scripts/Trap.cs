﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Game Over olacak.
            Debug.Log("GAME OVER(TUZAK).");
        }
    }
}
