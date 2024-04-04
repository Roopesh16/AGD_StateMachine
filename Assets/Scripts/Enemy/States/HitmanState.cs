using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class HitmanState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public HitmanState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        
        public void OnStateEnter()
        {
            TeleportToRandomPosition();
            stateMachine.ChangeState(States.CHASING);
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        private void TeleportToRandomPosition() => Owner.Agent.Warp(GetRandomNavMeshPoint());

        private Vector3 GetRandomNavMeshPoint()
        {
            Vector3 randomPosition = Random.insideUnitSphere * Owner.Data.TeleportingRadius + Owner.Position;
            NavMeshHit hit;

            // Validate random position
            if (NavMesh.SamplePosition(randomPosition, out hit, Owner.Data.TeleportingRadius, NavMesh.AllAreas))
                return hit.position;

            return Owner.Data.SpawnPosition;
        }
    }
}