namespace StatePattern.Enemy
{
    public class PatrolState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;

        public PatrolState(IStateMachine stateMachine) => this.stateMachine = stateMachine;
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