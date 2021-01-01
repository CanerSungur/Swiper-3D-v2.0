using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (player.transform.position.z - transform.position.z >= 20) Destroy(gameObject);
    }
}
