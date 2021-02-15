
public class TileHandler
{
    /*
     * 
     * This script will give info about where to spawn new rows.
     * 
     */

    private int rowToSpawn;

    public TileHandler(int lastRowInScene)
    {
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
