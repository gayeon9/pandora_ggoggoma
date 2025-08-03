using Unity.VisualScripting;
using UnityEngine;

namespace SuperEasyRPG
{
    public class InteractiveObject : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Interact()
        {
            Debug.Log($"Interact with {name}");
            EventBus.Trigger(EventNames.InteractionEvent, this);
        }
    }
}