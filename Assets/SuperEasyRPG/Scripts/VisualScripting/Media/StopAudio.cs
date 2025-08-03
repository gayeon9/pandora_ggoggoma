using Unity.VisualScripting;
using UnityEngine;

namespace SuperEasyRPG
{
    [UnitTitle("Stop Audio")]
    [UnitCategory("Super Easy RPG/Media")]
    public class StopAudio : Unit
    {

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput invoke;

        [DoNotSerialize]
        public ValueInput fadeOutDelaySeconds;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public ControlOutput onAudioStop;

        private GraphReference graphRef;

        protected override void Definition()
        {
            invoke = ControlInput(nameof(invoke), OnInvoke);
            exit = ControlOutput(nameof(exit));
            onAudioStop = ControlOutput(nameof(onAudioStop));

            fadeOutDelaySeconds = ValueInput(nameof(fadeOutDelaySeconds), 0.0);

            Succession(invoke, exit);
            Succession(invoke, onAudioStop);
        }

        private ControlOutput OnInvoke(Flow flow)
        {
            graphRef = flow.stack.AsReference();

            float fadeOutDelayValue = flow.GetValue<float>(fadeOutDelaySeconds);

            MediaPlayer mediaPlayer = ObjectUtil.FindSingletonObjectOfType<MediaPlayer>();
            mediaPlayer.StopAudio(
                fadeOutDelayValue,
                () => {
                    Flow.New(graphRef).Invoke(onAudioStop);
                }
            );

            return exit;
        }
    }
}
