using UnityEngine;
using System.Collections;
using System;

namespace App.TheValleyChase.Cameras {

    public class GameIntroCameraMovement : MonoBehaviour {
        public float positionThreshold;
        public float rotationThreshold;

        public Transform[] targets;
        public float[] movementSpeed;

        private int index;
        private bool introStarted;
        private bool introEnded;

        void Update() {

            if (UnityEngine.Input.GetButtonDown("Jump")) {
                StartIntro();
            }

            if (introStarted && index < targets.Length) {
                transform.position = Vector3.Lerp(transform.position, targets[index].position, movementSpeed[index] * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, targets[index].rotation, movementSpeed[index] * Time.deltaTime);

                if ((Vector3.Magnitude(transform.position - targets[index].position) <= positionThreshold)
                    && (Mathf.Abs(Quaternion.Angle(transform.rotation, targets[index].rotation)) <= rotationThreshold)) {
                    index++;
                }
            }


            if (index >= targets.Length) {
                introEnded = true;
            }
        }


        public void StartIntro() {
            introStarted = true;
        }

        public bool HasIntroEnded() {
            return introEnded;
        }
    }
}