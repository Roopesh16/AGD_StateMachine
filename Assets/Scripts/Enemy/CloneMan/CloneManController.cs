using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy.CloneMan
{
    public class CloneManController :  EnemyController
    {
        private CloneManStateMachine stateMachine;
        
        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);  
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new CloneManStateMachine(this);

        public override void UpdateEnemy()
        {
            if(currentState == EnemyState.DEACTIVE)
                return;
            
            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);

        public override void Shoot()
        {
            base.Shoot();
            stateMachine.ChangeState(States.TELEPORTING);
        }

        public override void Die()
        {
            stateMachine.ChangeState(States.CLONING);
            base.Die();
        }
    }
}