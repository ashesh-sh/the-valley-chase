using UnityEngine;
using System.Collections;
using App.TheValleyChase.Player;

namespace App.TheValleyChase.Cameras {

    public class InitialGameCameraMotion : MonoBehaviour {

        public float timeScaleSlow = 0.5f;
        public float motionTimer = 1f;

        public PlayerMovement playerMovement;
        public Transform initalPosition;

        private Vector3 finalPosition;
        private Quaternion finalRotation;

        private bool motionStarted;

        void Awake() {
            finalPosition = transform.localPosition;
            finalRotation = transform.localRotation;
        }

        void Start() {
            StartInitialMotion();
        }

        void Update() {
            if (UnityEngine.Input.GetButtonDown("Jump") && !motionStarted) {
                StartInitialMotion();
            }
        }

        public void StartInitialMotion() {
            playerMovement.DisablePlayerControl();

            transform.localPosition = initalPosition.localPosition;
            transform.localRotation = initalPosition.localRotation;

            Time.timeScale = timeScaleSlow;
            motionStarted = true;

            Invoke("StopInitialMotion", motionTimer);
        }

        public void StopInitialMotion() {
            playerMovement.EnablePlayerControl();

            Time.timeScale = 1f;
            motionStarted = false;

            transform.localPosition = finalPosition;
            transform.localRotation = finalRotation;
        }
    }
}