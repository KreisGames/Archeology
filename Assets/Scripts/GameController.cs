using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIController))]
public class GameController : MonoBehaviour, ISavable, ILoadable
{

    //Not property because serializefield
    [SerializeField][Range(0, 1)] private float treasureChance;
    
    [SerializeField] private uint maxNumberOfShowels;
    [SerializeField] private uint neededNumberOfTreasures;

    private UIController _uiController;
    
    private uint _numberOfShowels;
    private uint _numberOfTreasures;
    
    #region Unity Events
    
    private void Awake()
    {
        _numberOfShowels = maxNumberOfShowels;
        _uiController = GetComponent<UIController>();
    }

    private IEnumerator Start()
    { 
        //Setup for load system, gives time to load data and GUI system (GUI system is late call)
        yield return new WaitForEndOfFrame();
        _uiController.UpdateShowelsUI(_numberOfShowels, maxNumberOfShowels);
        _uiController.UpdateNumberOfTreasures(_numberOfTreasures, neededNumberOfTreasures);
    }

    #endregion
    
    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool HaveShowel()
    {
        return _numberOfShowels > 0;
    }

    public void DecreaseNumberOfShowels()
    {
        _numberOfShowels--;
        _uiController.UpdateShowelsUI(_numberOfShowels, maxNumberOfShowels);
    }

    //Getter
    public float GetTreasureChance()
    {
        return treasureChance;
    }

    public void AddTreasure()
    {
        _numberOfTreasures++;
        _uiController.UpdateNumberOfTreasures(_numberOfTreasures, neededNumberOfTreasures);
        if (_numberOfTreasures >= neededNumberOfTreasures)
        {
            _uiController.ShowVictoryScreen();
        }
    }
    
    #region SaveLoad Methods
    
    public void Save()
    {
        SaveManager.SaveCounts(_numberOfShowels, _numberOfTreasures);
    }

    public void Load()
    {
        (_numberOfShowels, _numberOfTreasures) = SaveManager.LoadCounts();
    }
    
    #endregion

}
