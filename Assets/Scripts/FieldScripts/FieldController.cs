using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace FieldScripts
{
    public class FieldController : MonoBehaviour, ISavable, ILoadable
    {

        [SerializeField] private Button[] buttons;
        [SerializeField] private GameController controller;

        [SerializeField] private GameObject treasure;

        private FieldTile[] _fieldTiles;

        private void Awake()
        {
            _fieldTiles = new FieldTile[buttons.Length];
            for (var i = 0; i < _fieldTiles.Length; i++)
            {
                _fieldTiles[i] = new FieldTile();
            }
        }

        private IEnumerator Start()
        {
            //Setup for load system, gives time to load all tiles ang GUI system (GUI system is late call)
            yield return new WaitForEndOfFrame();
            
            for (var i = 0; i < _fieldTiles.Length; i++)
            {
                if (_fieldTiles[i].HasTreasure)
                {
                    SpawnTreasure(i);
                }
                UpdateInteractability(i);
            }
        }

        public void Dig(int tileIndex)
        {
            _fieldTiles[tileIndex].Depth--;
            controller.DecreaseNumberOfShowels();
            if (!controller.HaveShowel())
            {
                foreach (var button in buttons)
                {
                    button.interactable = false;
                }
            }

            if (Random.Range(0f, 1f) < controller.GetTreasureChance())
            {
                SpawnTreasure(tileIndex);
            }

            UpdateInteractability(tileIndex);
        }

        private void SpawnTreasure(int tileIndex)
        {
            _fieldTiles[tileIndex].HasTreasure = true;
            //Transform.parent is canvas
            var treasureClone = Instantiate(treasure, transform.parent);
            treasureClone.transform.position = buttons[tileIndex].transform.position;
            treasureClone.GetComponent<Treasure>().Init(tileIndex, this);
        }

        private void UpdateInteractability(int tileIndex)
        {
            if (_fieldTiles[tileIndex].HasTreasure || _fieldTiles[tileIndex].Depth <= 0)
            {
                buttons[tileIndex].interactable = false;
            }
            else if (controller.HaveShowel())
            {
                buttons[tileIndex].interactable = true;
            }
        }

        public void TreasurePuttedInBag(int tileIndex)
        {
            controller.AddTreasure();
            _fieldTiles[tileIndex].HasTreasure = false;
            UpdateInteractability(tileIndex);
        }

        #region SaveLoad Methods

        public void Save()
        {
            for (var i = 0; i < _fieldTiles.Length; i++)
            {
                SaveManager.SaveTileData(i, _fieldTiles[i].Depth, _fieldTiles[i].HasTreasure);
            }
        }

        public void Load()
        {
            for (var i = 0; i < _fieldTiles.Length; i++)
            {
                (_fieldTiles[i].Depth, _fieldTiles[i].HasTreasure) = SaveManager.GetTileData(i);
            }
        }
        
        #endregion
        
    }
}
