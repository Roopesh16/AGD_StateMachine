using System.Collections.Generic;
using StatePattern.Main;
using StatePattern.Sound;
using StatePattern.StateMachine;
using UnityEngine;
using DG.Tweening;

namespace StatePattern.Enemy
{
    public class RoaringState<T> :  IState where T :  EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private float maxTime = 3f;
        private float shakeDuration = 0.2f;
        private float shakeStrength = 0.2f;
        
        public RoaringState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.ENEMY_ROAR);
            ShakeObstacles();
            SlowdownPlayer();
        }

        public void Update()
        {
            maxTime -= Time.deltaTime;

            if (maxTime <= 0)
                stateMachine.ChangeState(States.CHARGE);
        }

        public void OnStateExit() => GameService.Instance.PlayerService.GetPlayer().ResetPlayerSpeed();

        private void ShakeObstacles()
        {
            Collider[] obstacles;

            obstacles = Physics.OverlapSphere(Owner.Position, Owner.Data.RangeRadius);

            for (int i = 0; i < obstacles.Length; i++)
            {
                Shake(obstacles[i].gameObject);
            }
        }

        private void Shake(GameObject obstacle)
        {
            obstacle.transform.DOShakePosition(shakeDuration, shakeStrength).SetEase(Ease.InCirc);
            obstacle.transform.DOShakeScale(shakeDuration, shakeStrength).SetEase(Ease.InCirc);
        }

        private void SlowdownPlayer() => GameService.Instance.PlayerService.GetPlayer().MoveSpeed /= 2;
    }
}