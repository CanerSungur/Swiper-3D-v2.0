using UnityEngine;

public interface ITile
{
    GameObject Player { get; set; }
    int SpawnChance { get; set; }
    void DestroySelf();
}
