namespace StatePattern.Enemy.States
{
    public class ShootingState : IState
    {
        public OnePunchManController Owner { get; set; }
        public OnePunchManStateMachine StateMachine { get; set; }
        
        public ShootingState(OnePunchManStateMachine StateMachine)
        {
            this.StateMachine = StateMachine;
        }
        
        public void OnStateEnter()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }
    }
}