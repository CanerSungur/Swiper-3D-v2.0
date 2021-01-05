
public class TileHandler
{
    /*
     * 
     * This script will give info about where to spawn new rows.
     * 
     */
    //public static GameObject[] TilesToSpawn;

    private int lastRowInScene;
    private int rowToSpawn;

    public TileHandler(int lastRowInScene)
    {
        this.lastRowInScene = lastRowInScene;
        rowToSpawn = lastRowInScene + 1;
    }

    public int GetRowToSpawn()
    {
        return rowToSpawn;
    }

    public void UpdateRowToSpawn()
    {
        rowToSpawn++;
    }
}
