﻿using StatePattern.Enemy;
using StatePattern.Main;
using StatePattern.Sound;
using StatePattern.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerView playerView;

        private int currentHealth;
        public float MoveSpeed;
        private List<EnemyController> enemiesInRange;
        public Vector3 Position => playerView.transform.position;
        public UIService UIService => GameService.Instance.UIService;
        public SoundService SoundService => GameService.Instance.SoundService;
        public EnemyService EnemyService => GameService.Instance.EnemyService;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            MoveSpeed = playerScriptableObject.MovementSpeed;
            InitializeView();
            InitializeVariables();
        }

        public void DestroyPlayer() => Object.Destroy(playerView);

        private void InitializeView()
        {
            playerView = Object.Instantiate(playerScriptableObject.PlayerPrefab);
            playerView.transform.position = playerScriptableObject.SpawnPosition;
            playerView.transform.rotation = Quaternion.Euler(playerScriptableObject.SpawnRotation);
            playerView.SetController(this);
        }

        private void InitializeVariables()
        {
            currentHealth = playerScriptableObject.MaximumHealth;
            enemiesInRange = new List<EnemyController>();
            UIService.UpdatePlayerHealth((float)currentHealth / playerScriptableObject.MaximumHealth);
        }

        public void UpdatePlayer()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                UpdateAttack();
        }

        public void FixedUpdatePlayer() => UpdateMovement();

        private void UpdateMovement()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if(movementDirection != Vector3.zero)
            {
                RotatePlayer(movementDirection);
                MovePlayer(movementDirection);
            }
        }

        private void RotatePlayer(Vector3 movementDirection)
        {
            float targetRotation = GetTargetRotation(movementDirection);
            playerView.transform.eulerAngles = CalculateRotationToSet(targetRotation);
        }

        private float GetTargetRotation(Vector3 movementDirection) => Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        private Vector3 CalculateRotationToSet(float targetRotation) => Vector3.up * Mathf.MoveTowardsAngle(playerView.transform.eulerAngles.y, targetRotation, playerScriptableObject.RotationSpeed * Time.deltaTime);

        private void MovePlayer(Vector3 movementDirection)
        {
            Vector3 moveVector = GetMovementVector(movementDirection);
            playerView.Rigidbody.MovePosition(GetPositionToMoveAt(moveVector));
        }

        private Vector3 GetMovementVector(Vector3 movementDirection) => Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * movementDirection;

        private Vector3 GetPositionToMoveAt(Vector3 moveVector) => playerView.Rigidbody.position + moveVector * (MoveSpeed * Time.deltaTime);

        private void UpdateAttack()
        {
            playerView.PlayAttackVFX();
            if (enemiesInRange.Count > 0)
            {
               SoundService.PlaySoundEffects(SoundType.PLAYER_ATTACK);
                foreach (EnemyController enemy in enemiesInRange)
                {
                    enemy.Die();
                }
                enemiesInRange.Clear();
            }
            else
            {
                SoundService.PlaySoundEffects(SoundType.PLAYER_SLASH);
            }
        }

        public void TakeDamage(int damageToInflict)
        {
            currentHealth -= damageToInflict;
            SoundService.PlaySoundEffects(SoundType.PLAYER_HIT);
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                PlayerDied();
                EnemyService.PlayerDied();
            }
            UIService.UpdatePlayerHealth((float)currentHealth / playerScriptableObject.MaximumHealth);
        }

        private void PlayerDied()
        {
            SoundService.PlaySoundEffects(SoundType.GAME_LOST);
            UIService.GameLost();
        }

        public void AddEnemy(EnemyController enemy) => enemiesInRange.Add(enemy);
            
        public void RemoveEnemy(EnemyController enemy) => enemiesInRange.Remove(enemy);
        
        public void ResetPlayerSpeed() => MoveSpeed = playerScriptableObject.MovementSpeed;
    }
}