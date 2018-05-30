using UnityEngine;
using System.Collections;
using App.TheValleyChase.Player;
using UnityStandardAssets.ImageEffects;
using System;

namespace App.TheValleyChase.Cameras {

    public class DrowsyPlayerMovement : MonoBehaviour {

        public PlayerMovement playerMovement;

        private Camera mainCamera;
        private MotionBlur blurringComponent;
        private Fisheye fisheyeComponent;

        private float targetBlurAmount;
        private float targetFisheyeStrength;

        private bool isMotionBluring;

        private float blurThreshold = 0.05f;
        private float strengthThreshold = 0.05f;

        void Awake() {
            mainCamera = GetComponent<Camera>();
            blurringComponent = mainCamera.GetComponent<MotionBlur>();
            fisheyeComponent = mainCamera.GetComponent<Fisheye>();

            targetBlurAmount = blurringComponent.blurAmount;
            targetFisheyeStrength = fisheyeComponent.strengthX;
        }

        void Update() {
            CheckDrowsyState();

            if (isMotionBluring) {
                StartBlurring();
                StartFisheye();
            } else {
                StopBlurring();
                StopFisheye();
            }
        }

        private void CheckDrowsyState() {
            if (playerMovement.IsDrowsy() && !isMotionBluring) {
                isMotionBluring = true;
            } else if (!playerMovement.IsDrowsy()) {
                isMotionBluring = false;
            }
        }

        private void StartFisheye() {
            if (!fisheyeComponent.enabled) {
                fisheyeComponent.enabled = true;
            }

            if(fisheyeComponent.strengthX <= targetFisheyeStrength - strengthThreshold) {
                float newStrength = Mathf.Lerp(fisheyeComponent.strengthX, targetFisheyeStrength, Time.deltaTime * 4f);
                fisheyeComponent.strengthX = newStrength;
                fisheyeComponent.strengthY = newStrength;
            }
        }

        private void StopFisheye() {
            if (fisheyeComponent.strengthX >= strengthThreshold) {
                float newStrength = Mathf.Lerp(fisheyeComponent.strengthX, 0f, Time.deltaTime * 4f);
                fisheyeComponent.strengthX = newStrength;
                fisheyeComponent.strengthY = newStrength;
            } else {
                fisheyeComponent.strengthX = 0f;
                fisheyeComponent.strengthY = 0f;
                fisheyeComponent.enabled = false;
            }
        }
        

        private void StartBlurring() {
            if (!blurringComponent.enabled) {
                blurringComponent.enabled = true;
            }

            if (blurringComponent.blurAmount <= targetBlurAmount - blurThreshold) {
                blurringComponent.blurAmount = Mathf.Lerp(blurringComponent.blurAmount, targetBlurAmount, Time.deltaTime * 4f);
            }
        }

        private void StopBlurring() {
            if (blurringComponent.blurAmount >= blurThreshold) {
                blurringComponent.blurAmount = Mathf.Lerp(blurringComponent.blurAmount, 0f, Time.deltaTime * 4f);
            } else {
                blurringComponent.blurAmount = 0f;
                blurringComponent.enabled = false;
            }
        }
    }
}