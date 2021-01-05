using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
     * 
     * Makes row spawn when player gets passes by the spawn trigger line.
     * Holds info about player's state and which ground is player on.
     * Switch to relevant state.
     * Switch to relevant ground.
     * 
     * To change Player's speed, we alter animation speedRate.
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

    public enum Zone
    {
        Grass,
        Swamp,
        Forest_1,
        Forest_2,
        Forest_3,
        DangerousForest
    }

    public event EventHandler OnSpawnLinePassed;
    public event EventHandler OnGroundChange;

    public static State state;
    public static Zone zone;

    private Vector3 currentPosition;
    private int spawnTriggerLine;
    private float speedRate;

    private void Start()
    {
        spawnTriggerLine = 5;
        speedRate = 1f;

        state = State.Idle;
        zone = Zone.Forest_1;
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

    private void OnTriggerEnter(Collider other)
    {
        IMovementRestraint mrTile = other.GetComponent<IMovementRestraint>();
        if (mrTile != null)
        {
            mrTile.Slow();
        }

        OnGroundChange?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerExit(Collider other)
    {
        SetPlayerSpeedRate();

        OnGroundChange?.Invoke(this, EventArgs.Empty);
    }

    public float GetPlayerSpeedRate()
    {
        return speedRate;
    }

    public void SetPlayerSpeedRate(float speedRate)
    {
        this.speedRate = speedRate;
    }

    public void SetPlayerSpeedRate()
    {
        speedRate = 1f;
    }
}

