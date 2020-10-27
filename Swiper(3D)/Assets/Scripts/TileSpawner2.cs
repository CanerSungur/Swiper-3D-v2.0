using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner2 : MonoBehaviour
{
    /*
     * 
     * It spawns tiles per unit in z direction according to Player position.
     *
     * TODO: Check Player's position constantly. If its Z axis changes, spawn a random row of tiles.
     * pastPos is 0f by default. lastPos will be postPos + 1 which is the point of spawning tiles.
     * currentPos is player's current position. 
     * 
     */

    [Header("TileSpawner Setup Field")]
    public GameObject tile;
    public GameObject player;
    public GameObject trap;

    [Header("Player Position Field")]
    private Vector3 pastPos;
    private Vector3 currentPos;
    private Vector3 lastPos;

    [Header("Tile Spawn Field")]
    private int rowToSpawn;
    private int lastRowInMap;
    private bool didRowSpawn = false;

    private void Start()
    {
        //Default position values.
        pastPos.z = 8f;
        lastPos.z = pastPos.z + 1f;

        //Default position to spawn tiles.
        lastRowInMap = 15;
        rowToSpawn = lastRowInMap + 1;
    }

    private void Update()
    {
        //Take players position all the time.
        currentPos = player.GetComponent<Transform>().position;

        if (currentPos.z >= lastPos.z)
        {
            //Spawn 1 row of tiles.
            SpawnRowOfTiles(rowToSpawn);
            //Delete far behind tiles.
            DeleteObjectsLeftBehind();
            
            //Last row is increased
            lastRowInMap++;
            rowToSpawn = lastRowInMap + 1;

            pastPos.z++;
            lastPos.z = pastPos.z + 1;

            didRowSpawn = false;
        }

    }

    private void SpawnRowOfTiles(int whereToSpawn)
    {
        if (!didRowSpawn)
        {
            //Orta Row
            Instantiate(tile, new Vector3(4, 0, whereToSpawn), transform.rotation);

            int leftRandom = Random.Range(1, 5);
            int rightRandom = Random.Range(6, 10);
            List<int> trapTileXAxis = new List<int>();

            //Left Row
            switch (leftRandom)
            {
                case 1:
                    Instantiate(tile, new Vector3(3, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(3);
                    break;
                case 2:
                    Instantiate(tile, new Vector3(3, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(2, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(3);
                    trapTileXAxis.Add(2);
                    break;
                case 3:
                    Instantiate(tile, new Vector3(3, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(2, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(1, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(3);
                    trapTileXAxis.Add(2);
                    trapTileXAxis.Add(1);
                    break;
                case 4:
                    Instantiate(tile, new Vector3(3, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(2, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(1, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(0, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(3);
                    trapTileXAxis.Add(2);
                    trapTileXAxis.Add(1);
                    trapTileXAxis.Add(0);
                    break;
                default:
                    Debug.Log("Default case.");
                    break;
            }

            //Right Row
            switch (rightRandom)
            {
                case 6:
                    Instantiate(tile, new Vector3(5, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(5);
                    break;
                case 7:
                    Instantiate(tile, new Vector3(5, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(6, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(5);
                    trapTileXAxis.Add(6);
                    break;
                case 8:
                    Instantiate(tile, new Vector3(5, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(6, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(7, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(5);
                    trapTileXAxis.Add(6);
                    trapTileXAxis.Add(7);
                    break;
                case 9:
                    Instantiate(tile, new Vector3(5, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(6, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(7, 0, whereToSpawn), transform.rotation);
                    Instantiate(tile, new Vector3(8, 0, whereToSpawn), transform.rotation);
                    trapTileXAxis.Add(5);
                    trapTileXAxis.Add(6);
                    trapTileXAxis.Add(7);
                    trapTileXAxis.Add(8);
                    break;
                default:
                    Debug.Log("Default case.");
                    break;
            }

            //Decide trap or not.
            //SpawnWithTrap();
            if (SpawnWithTrap())
            {
                //Yukarıda listesini yaptığımız X Axis değerleri arasından random bir değer çekelim.
                Instantiate(trap, new Vector3(trapTileXAxis[Random.Range(0, trapTileXAxis.Count)], 0, whereToSpawn), transform.rotation);
            }
        }

        didRowSpawn = true;
    }

    private void DeleteObjectsLeftBehind()
    {
        GameObject[] tilesLeftBehind = GameObject.FindGameObjectsWithTag("Tile");
        GameObject[] trapsLeftBehind = GameObject.FindGameObjectsWithTag("Trap");

        foreach (var tile in tilesLeftBehind)
        {
            if ((player.transform.position.z - tile.transform.position.z) >= 20)
            {
                Destroy(tile);
            }
        }
    }

    private bool SpawnWithTrap()
    {
        int dice = Random.Range(0, 10);
        if (dice == 5)
            return true;
        else
            return false;
    }
}
