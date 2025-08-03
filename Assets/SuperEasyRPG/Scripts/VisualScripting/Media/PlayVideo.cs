using Unity.VisualScripting;
using UnityEngine.Video;

namespace SuperEasyRPG
{
    [UnitTitle("Play Video")]
    [UnitCategory("Super Easy RPG/Media")]
    public class PlayVideo : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput invoke;

        [DoNotSerialize]
        public ValueInput video;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public ControlOutput onVideoEnd;

        private GraphReference graphRef;

        protected override void Definition()
        {
            invoke = ControlInput(nameof(invoke), OnInvoke);
            exit = ControlOutput(nameof(exit));
            onVideoEnd = ControlOutput(nameof(onVideoEnd));

            video = ValueInput<VideoClip>(nameof(video), null);

            Succession(invoke, exit);
            Succession(invoke, onVideoEnd);
            Requirement(video, invoke);
        }

        private ControlOutput OnInvoke(Flow flow)
        {
            graphRef = flow.stack.AsReference();

            VideoClip videoClip = flow.GetValue<VideoClip>(video);

            MediaPlayer mediaPlayer = ObjectUtil.FindSingletonObjectOfType<MediaPlayer>();
            mediaPlayer.PlayVideo(videoClip, (_) => {
                Flow.New(graphRef).Invoke(onVideoEnd);
            });

            return exit;
        }
    }
}
