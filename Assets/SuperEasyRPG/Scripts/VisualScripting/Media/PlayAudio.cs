using Unity.VisualScripting;
using UnityEngine;

namespace SuperEasyRPG
{
    [UnitTitle("Play Audio")]
    [UnitCategory("Super Easy RPG/Media")]
    public class PlayAudio : Unit
    {

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput invoke;

        [DoNotSerialize]
        public ValueInput audio;

        [DoNotSerialize]
        public ValueInput loop;

        [DoNotSerialize]
        public ValueInput volume;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public ControlOutput onAudioEnd;

        private GraphReference graphRef;

        protected override void Definition()
        {
            invoke = ControlInput(nameof(invoke), OnInvoke);
            exit = ControlOutput(nameof(exit));
            onAudioEnd = ControlOutput(nameof(onAudioEnd));

            audio = ValueInput<AudioClip>(nameof(audio), null);
            loop = ValueInput(nameof(loop), false);
            volume = ValueInput(nameof(volume), 1.0);

            Succession(invoke, exit);
            Succession(invoke, onAudioEnd);
            Requirement(audio, invoke);
        }

        private ControlOutput OnInvoke(Flow flow)
        {
            graphRef = flow.stack.AsReference();

            AudioClip audioClip = flow.GetValue<AudioClip>(audio);
            float volumeValue = flow.GetValue<float>(volume);

            MediaPlayer mediaPlayer = ObjectUtil.FindSingletonObjectOfType<MediaPlayer>();
            mediaPlayer.PlayAudio(
                audioClip, 
                flow.GetValue<bool>(loop), 
                volumeValue,
                (_) => {
                    Flow.New(graphRef).Invoke(onAudioEnd);
                }
            );

            return exit;
        }
    }
}
