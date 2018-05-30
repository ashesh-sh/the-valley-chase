using App.TheValleyChase.GameController;
using App.TheValleyChase.PowerUps;
using System;
using UnityEngine;

namespace App.TheValleyChase.Player {

    public class PlayerStamina : MonoBehaviour {
        private bool isDecreasing;
        private bool overdosed;
        
        private float stamina;
        private float decreaseTime = 0.5f;
        private float overdosedTime;
        private float maxOverdoseEffectTime = 3f;

        private int overdoseCount;
        private int minOverdoseEffectCount = 3;

        private PlayerMovement movement;
        private GameStateManager stateManager;

        public float startStamina = 100f;
        public float decreaseRate = 0.25f;

        void Awake() {
            movement = GetComponent<PlayerMovement>();

            isDecreasing = false;
        }

        void Start() {
            stamina = startStamina;
            InvokeRepeating("DecreaseStamina", decreaseTime, decreaseTime);

            stateManager = GameController.GameController.Instance.GetStateManager();
        }

        void Update() {
            CheckStaminaRunOut();

            if (overdosed) {
                UpdateOverDosedTime();
            }
        }

        private void UpdateOverDosedTime() {
            overdosedTime += Time.deltaTime;

            if(overdosedTime >= maxOverdoseEffectTime) {
                overdosed = false;
                overdosedTime = 0f;
            }
        }

        private void CheckStaminaRunOut() {
            if(stamina <= 0) {
                stateManager.OnStaminaRunOut();
            }
        }

        public void StartStaminaDecreasing() {
            isDecreasing = true;
        }

        void DecreaseStamina() {
            if (isDecreasing) {
                stamina -= decreaseRate;
            }
        }

        public void IncreaseStamina(PowerUp powerUp) {
            stamina += powerUp.staminaIncreaseAmount;
            ManageStaminaOverdose();
            stamina = Mathf.Min(stamina,startStamina);
        }

        void ManageStaminaOverdose() {
            if (stamina > startStamina) {
                overdosed = true;

                overdoseCount++;

                if(overdoseCount >= minOverdoseEffectCount) {
                    overdosed = false;
                    overdosedTime = 0f;
                    overdoseCount = 0;

                    movement.StartDrowsyMovement();
                }
            }
        }

        public bool IsDecreasing() {
            return isDecreasing;
        }

        public void StopDecreasing() {
            isDecreasing = false;
        }

        public float GetStamina() {
            return stamina;
        }
    }
}