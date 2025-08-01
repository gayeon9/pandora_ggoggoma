using Unity.VisualScripting;

namespace SuperEasyRPG
{
    [UnitTitle("On Interaction")]
    [UnitCategory("Events/Super Easy RPG")]
    public class OnInteraction : EventUnit<InteractiveObject>
    {
        [DoNotSerialize]
        public ValueInput objectName;

        [DoNotSerialize]
        public ValueOutput interactiveObject;

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.InteractionEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            objectName = ValueInput(nameof(objectName), "");
            interactiveObject = ValueOutput<InteractiveObject>(nameof(interactiveObject));
        }

        protected override void AssignArguments(Flow flow, InteractiveObject data)
        {
            flow.SetValue(interactiveObject, data);
        }

        protected override bool ShouldTrigger(Flow flow, InteractiveObject data)
        {
            string objectNameVal = flow.GetValue<string>(objectName);
            return objectNameVal == data.name;
        }
    }
}