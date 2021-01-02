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
     */

    public enum State
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Freeze
    }

    public enum Ground
    {
        Grass,
        Dirt
    }

    public event EventHandler OnSpawnLinePassed;
    public event EventHandler OnGroundChange;
    public static State state;
    public static Ground ground;

    private Vector3 currentPosition;
    private int spawnTriggerLine;
    private float speeRate;

    private void Start()
    {
        spawnTriggerLine = 5;
        speeRate = 1f;

        state = State.Idle;
        ground = Ground.Grass;
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
        //ground = Ground.Dirt;
        
        IMovementRestraint mrTile = other.GetComponent<IMovementRestraint>();
        if (mrTile != null)
        {
            mrTile.Slow();
        }

        OnGroundChange(this, EventArgs.Empty);
    }

    private void OnTriggerExit(Collider other)
    {
        //ground = Ground.Grass;
        
        speeRate = 1f;

        OnGroundChange(this, EventArgs.Empty);
    }

    public float GetPlayerSpeedRate()
    {
        return speeRate;
    }

    public void SetPlayerSpeedRate(float speedRate)
    {
        this.speeRate = speedRate;
    }
}
