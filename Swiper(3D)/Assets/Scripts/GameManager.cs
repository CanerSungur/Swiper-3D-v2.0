using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Player.state == Player.State.Falling || Player.state == Player.State.Freeze)
        {
            Debug.Log("GAME OVER.");
        }
    }
}
