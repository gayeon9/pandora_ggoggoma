using Unity.VisualScripting;
using UnityEngine;

namespace SuperEasyRPG
{
    [UnitTitle("Get Player")]
    [UnitCategory("Super Easy RPG/Entities")]
    public class GetPlayer : Unit
    {
        [DoNotSerialize] public ValueOutput Player;

        protected override void Definition()
        {
            Player = ValueOutput<GameObject>("Player", flow =>
            {
                var Players = Object.FindObjectsByType<Player>(FindObjectsSortMode.None);

                // 첫 번째 Player를 p로 지정
                var p = Players.Length > 0 ? Players[0] : null;

                Debug.Log("Get Player found: " + (p != null ? p.name : "null"));

                return p ? p.gameObject : null;

            });
        }
    }
}
