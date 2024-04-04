using StatePattern.Enemy.CloneMan;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloneState<T> :  IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloneState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            SpawnClones();
            stateMachine.ChangeState(States.TELEPORTING);
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        private void SpawnClones()
        {
            EnemyController enemy1 = new EnemyController(Owner.Data);
            EnemyController enemy2 = new EnemyController(Owner.Data);
            stateMachine.ChangeState(States.TELEPORTING);
        }
    }
}