using Main.Gameplay.CommandSystem;
using Main.Gameplay.Enums;
using Main.Gameplay.TouchControls;
using Main.Utilities;
using System;
using UnityEngine;

namespace Main.Gameplay.StateMachineSystem
{
    public class StateMachine : Singleton<StateMachine>
    {
        public static event Action<StateBase> OnStateChanged;

        [field: SerializeField] public TouchControl Input { get; private set; }

        public TouchState TouchState { get; private set; }
        public AnimationState AnimationState { get; private set; }

        StateBase currentState;

        protected override void Awake()
        {
            base.Awake();

            TouchState = new TouchState(this);
            AnimationState = new AnimationState(this);

            currentState = TouchState;
        }

        public void ChangeState(StateBase nextState)
        {
            if (currentState == nextState) return;
            currentState.ExitState();
            currentState = nextState;
            currentState.EnterState();
            OnStateChanged?.Invoke(currentState);
        }
    }
}