using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
     * 
     * Makes row spawn when player gets passes by the spawn trigger line.
     * Holds info about player's state.
     * Switch to relevant state.
     * 
     */

    public enum State
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Freeze
    }

    public event EventHandler OnSpawnLinePassed;
    public static State state;

    private Vector3 currentPosition;
    private int spawnTriggerLine;

    private void Start()
    {
        spawnTriggerLine = 5;        
    }

    private void Update()
    {
        currentPosition = transform.position;

        if (currentPosition.z >= spawnTriggerLine)
        {
            // Trigger to Spawn Tiles
            OnSpawnLinePassed?.Invoke(this, EventArgs.Empty);
            spawnTriggerLine++;
        }


        #region State Changes

        if (transform.position.y < -1f && transform.position.y > -15f)
        {
            state = State.Falling;
        }
        else if (transform.position.y <= -15f)
        {
            state = State.Freeze;
        }

        #endregion
    }
}
