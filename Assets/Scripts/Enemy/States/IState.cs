namespace StatePattern.Enemy.States
{
    public interface IState
    {
        public OnePunchManController Owner { get; set; }

        public void OnStateEnter();

        public void Update();

        public void OnStateExit();
    }
}