namespace StatePattern.Enemy.States
{
    public class IdleState : IState
    {
        public OnePunchManController Owner { get; set; }
        
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