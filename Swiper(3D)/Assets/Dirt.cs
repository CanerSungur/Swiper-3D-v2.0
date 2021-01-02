using UnityEngine;

public class Dirt : MonoBehaviour, ITile, IMovementRestraint
{
    /*
     * 
     * Changes player's speed if player is on Dirt.
     * 
     */

    public float slowRate { get; set; }
    public GameObject player { get; set; }

    private void Awake()
    {
        player = GameObject.Find("Player");
        slowRate = 0.5f; // 50% slow rate
    }

    private void Update()
    {
        DestroySelf();
    }

    public void DestroySelf()
    {
        if (player.transform.position.z - transform.position.z >= 20) Destroy(gameObject);
    }

    public void Slow()
    {
        player.GetComponent<Player>().SetPlayerSpeedRate(slowRate);
    }
}
