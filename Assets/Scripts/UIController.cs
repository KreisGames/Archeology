using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    [SerializeField] private Text numberOfShowelsUI;
    [SerializeField] private Text numberOfTreasuresUI;
    
    [SerializeField] private GameObject victoryCanvas;

    public void UpdateShowelsUI(uint currentShowels, uint maxShowels)
    {
        numberOfShowelsUI.text = currentShowels + "/" + maxShowels;
    }

    public void UpdateNumberOfTreasures(uint currentTreasures, uint maxTreasures)
    {
        numberOfTreasuresUI.text = currentTreasures + "/" + maxTreasures;
    }

    public void ShowVictoryScreen()
    {
        victoryCanvas.SetActive(true);
    }

}
