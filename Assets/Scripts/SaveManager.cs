using UnityEngine;

public static class SaveManager
{

    //Set save data from previous attempt
    public static (uint, uint) LoadCounts()
    {
        return ((uint)PlayerPrefs.GetInt("NumberOfShowels"), (uint)PlayerPrefs.GetInt("NumberOfTreasures"));
    }

    //Save game to playerprefs
    public static void SaveCounts(uint numberOfShowels, uint numberOfTreasures)
    {
        PlayerPrefs.SetInt("NumberOfShowels", (int)numberOfShowels);
        PlayerPrefs.SetInt("NumberOfTreasures", (int)numberOfTreasures);
        PlayerPrefs.Save();
    }

    public static void SaveTileData(int tileIndex, int tileDepth, bool hasTreasure)
    {
        PlayerPrefs.SetInt(tileIndex + "Depth", tileDepth);
        PlayerPrefs.SetInt(tileIndex + "TreasureState", hasTreasure ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static (int, bool) GetTileData(int tileIndex)
    {
        return (PlayerPrefs.GetInt(tileIndex + "Depth"), PlayerPrefs.GetInt(tileIndex + "TreasureState") == 1);
    }

}
