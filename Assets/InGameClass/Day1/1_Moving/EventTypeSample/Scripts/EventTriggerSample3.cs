using UnityEngine;
using UnityEngine.EventSystems;

namespace EventTriggerExample
{
    public class EventTriggerSample3 : MonoBehaviour
    {
        void Start()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener(
                (data) => {
                    OnPointerDownDelegate((PointerEventData)data);
                });
            trigger.triggers.Add(entry);
        }

        public void OnPointerDownDelegate(PointerEventData data)
        {
            Debug.Log("OnPointerDownDelegate called.");
        }
    }
}
