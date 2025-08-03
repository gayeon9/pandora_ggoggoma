using UnityEngine;
using Unity.VisualScripting;

namespace SuperEasyRPG
{
    [UnitTitle("Show Sentence Dialog")]
    [UnitCategory("Super Easy RPG/Dialog")]
    public class ShowSentenceDialog : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput invoke;

        [DoNotSerialize]
        public ValueInput characterName;

        [DoNotSerialize]
        public ValueInput sentence;

        [DoNotSerialize]
        public ValueInput characterImage;

        [DoNotSerialize]
        public ValueInput autoHideDialog;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public ControlOutput onNext;


        private GraphReference graphRef;

        protected override void Definition()
        {
            invoke = ControlInput(nameof(invoke), OnInvoke);
            exit = ControlOutput(nameof(exit));

            characterName = ValueInput(nameof(characterName), "");
            sentence = ValueInput(nameof(sentence), "");
            characterImage = ValueInput<Sprite>(nameof(characterImage), null);

            onNext = ControlOutput(nameof(onNext));

            autoHideDialog = ValueInput(nameof(autoHideDialog), true); // ±âº»°ª true

            Succession(invoke, exit);
            Succession(invoke, onNext);


        }

        private ControlOutput OnInvoke(Flow flow)
        {
            graphRef = flow.stack.AsReference();

            string characterNameVal = flow.GetValue<string>(characterName);
            Sprite characterImageVal = flow.GetValue<Sprite>(characterImage);
            string sentenceVal = flow.GetValue<string>(sentence);
            bool autoHideDialogVal = flow.GetValue<bool>(autoHideDialog);

            SentenceDialog dialog = new(characterNameVal, characterImageVal, sentenceVal, autoHideDialogVal);

            Player player = ObjectUtil.FindSingletonObjectOfType<Player>();
            if (player != null)
            {
                player.canMove = false;
            }

            DialogUI ui = ObjectUtil.FindSingletonObjectOfType<DialogUI>(true);
            bool shouldHideDialog = flow.GetValue<bool>(autoHideDialog);
            ui.ShowSentenceDialog(dialog, () =>
            {
                if (player != null)
                {
                    player.canMove = true;
                }

                if (shouldHideDialog)
                {
                    SentenceDialogPanel.Instance.HideDialog();
                }

                Flow.New(graphRef).Invoke(onNext);
            });

            return exit;
        }
    }
}
