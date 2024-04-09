using StatePattern.StateMachine;

namespace StatePattern.Enemy.Titanis
{
    public class TitanisStateMachine : GenericStateMachine<TitanisController>
    {
        private TitanisController Owner;
        
        public TitanisStateMachine(TitanisController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState<TitanisController>(this));
            States.Add(StateMachine.States.PATROLLING,new PatrollingState<TitanisController>(this));
            States.Add(StateMachine.States.RAMPAGE,new RampageState<TitanisController>(this));
            States.Add(StateMachine.States.ROARING,new RoaringState<TitanisController>(this));
            States.Add(StateMachine.States.CHARGE,new ChargeState<TitanisController>(this));
            States.Add(StateMachine.States.ATTACK,new AttackState<TitanisController>(this));
        }
    }
}