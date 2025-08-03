using Unity.VisualScripting;

namespace SuperEasyRPG
{
    [UnitTitle("Get Player")]
    [UnitCategory("Super Easy RPG/Entities")]
    public class GetPlayer : Unit
    {
        [DoNotSerialize]
        public ValueOutput player;

        protected override void Definition()
        {
            player = ValueOutput(nameof(player), (flow) =>
            {
                return ObjectUtil.FindSingletonObjectOfType<Player>();
            });
        }
    }
}