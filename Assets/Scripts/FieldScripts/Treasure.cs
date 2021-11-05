using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FieldScripts
{
    public class Treasure : MonoBehaviour
    {

        private Vector3 _startingPosition;
        private bool _dragging;
        private int _index;
        private FieldController _fieldController;

        //Initialize method for instantiation purpose
        public void Init(int index, FieldController fieldController)
        {
            _startingPosition = transform.position;
            _index = index;
            _fieldController = fieldController;
        }
        
        public void BeginDrag()
        {
            _dragging = true;
        }

        public void Drag()
        {
            if (_dragging)
                transform.position = Input.mousePosition;
        }

        public void Drop()
        {
            if (!_dragging) return;
            _dragging = false;

            var pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
                position = Input.mousePosition
            };

            //Needed for lower layer ui work
            var hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, hits);
            
            var bagFound = hits.Any(hit => hit.gameObject.CompareTag("Bag"));

            if (bagFound)
            {
                _fieldController.TreasurePuttedInBag(_index);
                Destroy(gameObject);
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
    }
}
