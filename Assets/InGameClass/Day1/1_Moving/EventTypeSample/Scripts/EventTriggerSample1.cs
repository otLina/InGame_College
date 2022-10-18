using UnityEngine;

namespace EventTriggerExample
{
    public class EventTriggerSample1 : MonoBehaviour
    {
        public void OnBeginDrag()
        {
            Debug.Log("OnBeginDrag called.");
        }

        public void OnCancel()
        {
            Debug.Log("OnCancel called.");
        }

        public void OnDeselect()
        {
            Debug.Log("OnDeselect called.");
        }

        public void OnDrag()
        {
            Debug.Log("OnDrag called.");
        }

        public void OnDrop()
        {
            Debug.Log("OnDrop called.");
        }

        public void OnEndDrag()
        {
            Debug.Log("OnEndDrag called.");
        }

        public void OnInitializePotentialDrag()
        {
            Debug.Log("OnInitializePotentialDrag called.");
        }

        public void OnMove()
        {
            Debug.Log("OnMove called.");
        }

        public void OnPointerClick()
        {
            Debug.Log("OnPointerClick called.");
        }

        public void OnPointerDown()
        {
            Debug.Log("OnPointerDown called.");
        }

        public void OnPointerEnter()
        {
            Debug.Log("OnPointerEnter called.");
        }

        public void OnPointerExit()
        {
            Debug.Log("OnPointerExit called.");
        }

        public void OnPointerUp()
        {
            Debug.Log("OnPointerUp called.");
        }

        public void OnScroll()
        {
            Debug.Log("OnScroll called.");
        }

        public void OnSelect()
        {
            Debug.Log("OnSelect called.");
        }

        public void OnSubmit()
        {
            Debug.Log("OnSubmit called.");
        }

        public void OnUpdateSelected()
        {
            Debug.Log("OnUpdateSelected called.");
        }
    }
}