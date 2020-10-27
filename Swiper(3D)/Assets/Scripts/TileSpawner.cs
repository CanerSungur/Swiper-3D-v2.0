using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject trap;
    public GameObject player;
    public GameObject playerObject;
    private Vector3 playerPosition;
    private float zMaxPoint;
    private float zBuildPoint;
    private int roadWidth;
    private bool willSpawn = false;

    [Header("Objects To Spawn")]
    public GameObject grassPrefab;

    //For Spawning By Camera Transform
    private Vector3 cameraPosition;
    private float zMaxPointCamera;
    private float zBuildPointCamera;
    //Camera camera;

    void Start()
    {
        playerPosition = player.transform.position;
        zMaxPoint = playerPosition.z + 15;

        //cameraPosition = camera.WorldToScreenPoint(camera.position);
    }

    void Update()
    {
        playerPosition = player.transform.position;

        if (zMaxPoint - playerPosition.z <= 12)
        {
            if (willSpawn)
            {
                //Kutu Spawn et.
                SpawnTiles();
                willSpawn = false;
                return;
            }
            else
            {
                if (Input.GetKey(KeyCode.F))
                    willSpawn = true;
            }
        }
    }

    private void SpawnTiles()
    {
        zBuildPoint = playerPosition.z + 12;

        bool spawnWithTrap = false;
        int trapDice = Random.Range(0, 20);

        if (trapDice == 10)
            spawnWithTrap = true;
        else
            spawnWithTrap = false;
        Debug.Log(spawnWithTrap);

        //Orta Row
        Instantiate(tiles[0], new Vector3(3, 0, zBuildPoint - 1), transform.rotation);

        int leftRandom = Random.Range(1, 4);
        int rightRandom = Random.Range(5, 8);
        List<int> trapTileXAxis = new List<int>();
        
        //Left Row
        switch (leftRandom)
        {
            case 1:
                Instantiate(tiles[0], new Vector3(2, 0, zBuildPoint - 1), transform.rotation);
                trapTileXAxis.Add(2);
                break;
            case 2:
                Instantiate(tiles[0], new Vector3(2, 0, zBuildPoint - 1), transform.rotation);
                Instantiate(tiles[0], new Vector3(1, 0, zBuildPoint - 1), transform.rotation);
                trapTileXAxis.Add(2);
                trapTileXAxis.Add(1);
                break;
            case 3:
                Instantiate(tiles[0], new Vector3(2, 0, zBuildPoint - 1), transform.rotation);
                Instantiate(tiles[0], new Vector3(1, 0, zBuildPoint - 1), transform.rotation);
                Instantiate(tiles[0], new Vector3(0, 0, zBuildPoint - 1), transform.rotation);
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
            case 5:
                Instantiate(tiles[0], new Vector3(4, 0, zBuildPoint - 1), transform.rotation);
                trapTileXAxis.Add(4);
                break;
            case 6:
                Instantiate(tiles[0], new Vector3(4, 0, zBuildPoint - 1), transform.rotation);
                Instantiate(tiles[0], new Vector3(5, 0, zBuildPoint - 1), transform.rotation);
                trapTileXAxis.Add(4);
                trapTileXAxis.Add(5);
                break;
            case 7:
                Instantiate(tiles[0], new Vector3(4, 0, zBuildPoint - 1), transform.rotation);
                Instantiate(tiles[0], new Vector3(5, 0, zBuildPoint - 1), transform.rotation);
                Instantiate(tiles[0], new Vector3(6, 0, zBuildPoint - 1), transform.rotation);
                trapTileXAxis.Add(4);
                trapTileXAxis.Add(5);
                trapTileXAxis.Add(6);
                break;
            default:
                Debug.Log("Default case.");
                break;
        }

        if (spawnWithTrap)
        {
            //Yukarıda listesini yaptığımız X Axis değerleri arasından random bir değer çekelim.
            Instantiate(trap, new Vector3(trapTileXAxis[Random.Range(0, trapTileXAxis.Count)], 0, zBuildPoint - 1), transform.rotation);
        }
    }

    private void ControlSpaceBetweenTiles()
    {

    }
}
