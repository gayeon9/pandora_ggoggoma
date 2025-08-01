using Unity.VisualScripting;

namespace SuperEasyRPG
{
    [UnitTitle("On Leave Area")]
    [UnitCategory("Events/Super Easy RPG")]
    public class OnLeaveArea : EventUnit<TriggerArea>
    {
        [DoNotSerialize]
        public ValueInput areaName;

        [DoNotSerialize]
        public ValueOutput area;

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.LeaveAreaEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            areaName = ValueInput(nameof(areaName), "");
            area = ValueOutput<TriggerArea>(nameof(area));
        }

        protected override void AssignArguments(Flow flow, TriggerArea data)
        {
            flow.SetValue(area, data);
        }

        protected override bool ShouldTrigger(Flow flow, TriggerArea data)
        {
            string eventNameVal = flow.GetValue<string>(areaName);
            return eventNameVal == data.name;
        }
    }
}