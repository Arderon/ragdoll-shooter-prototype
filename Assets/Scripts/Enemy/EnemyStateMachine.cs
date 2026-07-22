using System;

namespace EnemyAI
{
    public class EnemyStateMachine
    {
        public IState CurrentState { get; private set; }

        public event Action<IState, IState> OnStateChanged; // (oldState, newState)

        public void ChangeState(IState newState)
        {
            if (newState == null || newState == CurrentState)
                return;

            var oldState = CurrentState;

            oldState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();

            OnStateChanged?.Invoke(oldState, CurrentState);
        }

        public void Tick() => CurrentState?.Tick();
        public void FixedTick() => CurrentState?.FixedTick();
    }
}