using Main.Gameplay.TouchControls;
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

            Input.OnSwipeInputRecieved += CompleteTouchState;
        }

        public void ChangeState(StateBase nextState)
        {
            if (currentState == nextState) return;
            currentState.ExitState();
            currentState = nextState;
            currentState.EnterState();
            OnStateChanged?.Invoke(currentState);
        }

        private void CompleteTouchState()
        {
            ChangeState(AnimationState);
        }

        private void OnDisable()
        {
            Input.OnSwipeInputRecieved -= CompleteTouchState;
        }
    }
}