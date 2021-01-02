using System;
using UnityEngine;

public interface ITile
{
    GameObject player { get; set; }
    void DestroySelf();
}
