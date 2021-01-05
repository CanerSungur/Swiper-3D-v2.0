using System.Collections.Generic;
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
    public Player player;
    public GameObject trap;

    [Header("Zone Prefabs Setup")]
    public GameObject[] grassZonePrefabs;
    public GameObject forestZonePrefab_1;
    public GameObject forestZonePrefab_2;
    public GameObject forestZonePrefab_3;
    //public GameObject[] dangerousForestZonePrefabs;

    public ITile[] exampleTilePrefabs;

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
            Instantiate(PickRandomTile(grassZonePrefabs), new Vector3(startingXAxis + i, 0, position), Quaternion.identity);
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

    private GameObject PickRandomTile(GameObject[] tilesToPick)
    {
        int r = RandomNumber(0, 100);

        List<GameObject> highChance = new List<GameObject>();
        List<GameObject> midChance = new List<GameObject>();
        List<GameObject> lowChance = new List<GameObject>();

        foreach (var tile in tilesToPick)
        {
            if (tile.GetComponent<ITile>().SpawnChance == 1)
                highChance.Add(tile);
            else if (tile.GetComponent<ITile>().SpawnChance == 2)
                midChance.Add(tile);
            else
                lowChance.Add(tile);
        }

        if (r >= 0 && r <= 85)
            return highChance[Random.Range(0, highChance.Count)];
        else if (r > 85 && r <= 98)
            return midChance[Random.Range(0, midChance.Count)];
        else
        {
            // if we are not in forest zone.
            if (lowChance == null)
                return highChance[Random.Range(0, highChance.Count)];
            else
                return lowChance[Random.Range(0, lowChance.Count)];
        }
    }

    private int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
