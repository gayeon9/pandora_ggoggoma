using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace SuperEasyRPG
{
    public class TriggerArea : MonoBehaviour
    {
        public UnityEvent onEnterArea;
        public UnityEvent onLeaveArea;

        public bool triggerOnlyOnce = false;
        public bool invisible = true;

        private bool alreadyEntered = false;
        private bool alreadyLeaved = false;

        void Start()
        {
            if (invisible)
            {
                GetComponent<SpriteRenderer>().color = Color.clear;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (triggerOnlyOnce && alreadyEntered)
            {
                return;
            }

            if (col.gameObject.GetComponent<Player>() != null)
            {
                Debug.Log($"Player entered area: {name}");
                alreadyEntered = true;
                onEnterArea.Invoke();
                EventBus.Trigger(EventNames.EnterAreaEvent, this);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (triggerOnlyOnce && alreadyLeaved)
            {
                return;
            }

            if (col.gameObject.GetComponent<Player>() != null)
            {
                Debug.Log($"Player leaved area: {name}");
                alreadyLeaved = true;
                onLeaveArea.Invoke();
                EventBus.Trigger(EventNames.LeaveAreaEvent, this);
            }
        }
    }
}