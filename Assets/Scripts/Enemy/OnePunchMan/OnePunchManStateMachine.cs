using StateMachine;
using System.Collections.Generic;
using StatePattern.StateMachine;
using StatePattern.Utilities;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : GenericStateMachine<OnePunchManController>
    {
        private OnePunchManController Owner;
        private IState currentState;
        protected Dictionary<States, IState> states = new Dictionary<States, IState>();

        public OnePunchManStateMachine(OnePunchManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(States.IDLE, new IdleState<OnePunchManController>(this));
            states.Add(States.ROTATING, new RotatingState<OnePunchManController>(this));
            states.Add(States.SHOOTING, new ShootingState<OnePunchManController>(this));
        }

        private void SetOwner()
        {
            foreach(IState state in states.Values)
            {
                state.Owner = Owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(States newState) => ChangeState(states[newState]);
    }
}