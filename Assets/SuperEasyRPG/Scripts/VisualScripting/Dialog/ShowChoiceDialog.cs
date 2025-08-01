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

        [DoNotSerialize]
        public ValueInput choice1;

        [DoNotSerialize]
        public ValueInput choice2;

        [DoNotSerialize]
        public ValueInput choice3;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public ControlOutput output1;

        [DoNotSerialize]
        public ControlOutput output2;

        [DoNotSerialize]
        public ControlOutput output3;

        private GraphReference graphRef;

        protected override void Definition()
        {
            invoke = ControlInput(nameof(invoke), OnInvoke);
            exit = ControlOutput(nameof(exit));

            choice1 = ValueInput(nameof(choice1), "");
            choice2 = ValueInput(nameof(choice2), "");
            choice3 = ValueInput(nameof(choice3), "");

            output1 = ControlOutput(nameof(output1));
            output2 = ControlOutput(nameof(output2));
            output3 = ControlOutput(nameof(output3));

            Succession(invoke, exit);
            Succession(invoke, output1);
            Succession(invoke, output2);
            Succession(invoke, output3);
        }

        private ControlOutput OnInvoke(Flow flow)
        {
            graphRef = flow.stack.AsReference();

            string choice1Val = flow.GetValue<string>(choice1);
            string choice2Val = flow.GetValue<string>(choice2);
            string choice3Val = flow.GetValue<string>(choice3);

            List<string> choices = new();
            List<System.Action> listeners = new();
            if (choice1Val != "")
            {
                choices.Add(choice1Val);
            }

            if (choice2Val != "")
            {
                choices.Add(choice2Val);
            }

            if (choice3Val != "")
            {
                choices.Add(choice3Val);
            }

            Player player = ObjectUtil.FindSingletonObjectOfType<Player>();
            if (player != null)
            {
                player.canMove = false;
            }

            DialogUI ui = ObjectUtil.FindSingletonObjectOfType<DialogUI>(true);
            ui.ShowChoiceDialog(choices.ToArray(), (selectedChoice, index) =>
            {
                Debug.Log($"Choice {selectedChoice} selected");

                if (player != null)
                {
                    player.canMove = true;
                }

                if (selectedChoice == choice1Val)
                {
                    Flow.New(graphRef).Invoke(output1);
                }
                else if (selectedChoice == choice2Val)
                {
                    Flow.New(graphRef).Invoke(output2);
                }
                else if (selectedChoice == choice3Val)
                {
                    Flow.New(graphRef).Invoke(output3);
                }
                else
                {
                    Debug.LogError($"Unknown choice selected: choice={selectedChoice}, index={index}");
                }
            });

            return exit;
        }
    }
}