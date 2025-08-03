using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace SuperEasyRPG
{
    [UnitTitle("Show Choice Dialog")]
    [UnitCategory("Super Easy RPG/Dialog")]
    public class ShowChoiceDialog : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput invoke;

        [DoNotSerialize] public ValueInput choice1;
        [DoNotSerialize] public ValueInput choice2;
        [DoNotSerialize] public ValueInput choice3;
        [DoNotSerialize] public ValueInput choice4;
        [DoNotSerialize] public ValueInput choice5;

        [DoNotSerialize] public ValueInput choice1Condition;
        [DoNotSerialize] public ValueInput choice2Condition;
        [DoNotSerialize] public ValueInput choice3Condition;
        [DoNotSerialize] public ValueInput choice4Condition;
        [DoNotSerialize] public ValueInput choice5Condition;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize] public ControlOutput output1;
        [DoNotSerialize] public ControlOutput output2;
        [DoNotSerialize] public ControlOutput output3;
        [DoNotSerialize] public ControlOutput output4;
        [DoNotSerialize] public ControlOutput output5;

        private GraphReference graphRef;

        protected override void Definition()
        {
            invoke = ControlInput(nameof(invoke), OnInvoke);
            exit = ControlOutput(nameof(exit));

            choice1 = ValueInput(nameof(choice1), "");
            choice2 = ValueInput(nameof(choice2), "");
            choice3 = ValueInput(nameof(choice3), "");
            choice4 = ValueInput(nameof(choice4), "");
            choice5 = ValueInput(nameof(choice5), "");

            choice1Condition = ValueInput(nameof(choice1Condition), true);
            choice2Condition = ValueInput(nameof(choice2Condition), true);
            choice3Condition = ValueInput(nameof(choice3Condition), true);
            choice4Condition = ValueInput(nameof(choice4Condition), true);
            choice5Condition = ValueInput(nameof(choice5Condition), true);

            output1 = ControlOutput(nameof(output1));
            output2 = ControlOutput(nameof(output2));
            output3 = ControlOutput(nameof(output3));
            output4 = ControlOutput(nameof(output4));
            output5 = ControlOutput(nameof(output5));

            Succession(invoke, exit);
            Succession(invoke, output1);
            Succession(invoke, output2);
            Succession(invoke, output3);
            Succession(invoke, output4);
            Succession(invoke, output5);
        }

        private ControlOutput OnInvoke(Flow flow)
        {
            graphRef = flow.stack.AsReference();

            List<string> choices = new();
            List<ControlOutput> outputMap = new();

            // 선택지와 조건을 조합하여 처리
            void TryAddChoice(ValueInput choiceInput, ValueInput conditionInput, ControlOutput outputPort)
            {
                string text = flow.GetValue<string>(choiceInput);
                bool condition = flow.GetValue<bool>(conditionInput);
                if (!string.IsNullOrEmpty(text) && condition)
                {
                    choices.Add(text);
                    outputMap.Add(outputPort);
                }
            }

            TryAddChoice(choice1, choice1Condition, output1);
            TryAddChoice(choice2, choice2Condition, output2);
            TryAddChoice(choice3, choice3Condition, output3);
            TryAddChoice(choice4, choice4Condition, output4);
            TryAddChoice(choice5, choice5Condition, output5);

            Player player = ObjectUtil.FindSingletonObjectOfType<Player>();
            if (player != null)
            {
                player.canMove = false;
            }

            DialogUI ui = ObjectUtil.FindSingletonObjectOfType<DialogUI>(true);
            ui.ShowChoiceDialog(choices.ToArray(), (selectedChoice, index) =>
            {
                if (player != null)
                {
                    player.canMove = true;
                }

                if (index >= 0 && index < outputMap.Count)
                {
                    Flow.New(graphRef).Invoke(outputMap[index]);
                }
                else
                {
                    Debug.LogError($"Invalid choice index: {index} for choice {selectedChoice}");
                }
            });

            return exit;
        }
    }
}
