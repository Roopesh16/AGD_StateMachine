using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy.Titanis
{
    public class TitanisController : EnemyController
    {
        private TitanisStateMachine stateMachine;
        
        public TitanisController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new TitanisStateMachine(this);

        public override void UpdateEnemy()
        {
            if(currentState == EnemyState.DEACTIVE)
                return;
            
            stateMachine?.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.ROARING);
        }

        public override void PlayerExitedRange()
        {
            stateMachine.ChangeState(States.IDLE);
        }
    }
}