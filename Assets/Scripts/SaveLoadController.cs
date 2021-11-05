using System.Linq;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    
    //Load all savable data after awake but before start of the game, to correctly initialize order
    private void Start()
    {
        //Check if save exists
        if (!PlayerPrefs.HasKey("NumberOfShowels")) return;
        //Call load method on all loadables
        var loadables = FindObjectsOfType<MonoBehaviour>().OfType<ILoadable>();
        foreach (var loadable in loadables)
        {
            loadable.Load();
        }
    }
    
    //Runs when game wants to exit
    [RuntimeInitializeOnLoadMethod]
    private static void QuitHandler()
    {
        Application.wantsToQuit += SaveAllData;
    }

    private static bool SaveAllData()
    {
        var savables = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>();
        foreach (var savable in savables)
        {
            savable.Save();
        }

        return true;
    }

}
