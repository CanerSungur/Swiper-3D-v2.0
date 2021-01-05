using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    /*
     * 
     * It spawns tiles randomly in z direction according to Player position.
     * Rolls a dice. If it's successful, spawns a trap on a random tile.
     * 
     * Last row's z axis is 15. we want to create rows when we reach mid range of the map.
     * 
     */

    [Header("TileSpawner Setup Field")]
    //public GameObject[] tilePrefabs;
    public Player player;
    public GameObject trap;

    [Header("Zone Prefabs Setup")]
    public GameObject[] grassZonePrefabs;
    public GameObject[] forestZonePrefabs_1;
    public GameObject[] forestZonePrefabs_2;
    public GameObject[] forestZonePrefabs_3;
    public GameObject[] dangerousForestZonePrefabs;

    TileHandler tileHandler;

    private void Start()
    {
        // Instantiate handler with last row's Z axis as parameter.
        tileHandler = new TileHandler(15);

        player.OnSpawnLinePassed += Player_OnSpawnLinePassed;
    }

    private void Player_OnSpawnLinePassed(object sender, System.EventArgs e)
    {
        // What happens when player passes the line.
        SpawnRow(tileHandler.GetRowToSpawn());
    }

    private void SpawnRow(int position)
    {
        // Decide how many tiles will be spawned.
        int tilesCount = Random.Range(6, 14);

        // Which x axis should it start to instantiate
        int startingXAxis = Random.Range(4, 8);

        for (int i = 0; i < tilesCount; i++)
        {
            Instantiate(forestZonePrefabs_1[Random.Range(0, forestZonePrefabs_1.Length)], new Vector3(startingXAxis + i, 0, position), Quaternion.identity);
        }

        tileHandler.UpdateRowToSpawn();
    }

    private bool TrapDice()
    {
        int dice = Random.Range(0, 10);
        if (dice == 5)
            return true;
        else
            return false;
    }
}
