using UnityEngine;

public class Swamp : MonoBehaviour, ITile, IMovementRestraint
{
    /*
     * 
     * Changes player's speed if player is on Dirt.
     * 
     */

    public float slowRate { get; set; }
    public GameObject Player { get; set; }
    public int SpawnChance { get { return spawnChance; } set { spawnChance = value; } }
    [SerializeField] private int spawnChance;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        slowRate = 0.5f; // 50% slow rate
    }

    private void Update()
    {
        DestroySelf();
    }

    public void DestroySelf()
    {
        if (Player.transform.position.z - transform.position.z >= 20) Destroy(gameObject);
    }

    public void Slow()
    {
        Player.GetComponent<Player>().SetPlayerSpeedRate(slowRate);
    }
}
