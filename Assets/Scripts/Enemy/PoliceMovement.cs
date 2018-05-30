using UnityEngine;
using System.Collections;

namespace App.TheValleyChase.Enemy {

    public class PoliceMovement : MonoBehaviour {
        public float thresholdPosition;

        private Transform playerTransform;
        private Animator animator;

        private bool catchPlayer;

        void Awake() {
            playerTransform = transform.parent;
            animator = GetComponent<Animator>();
        }

        void Update() {
            if (catchPlayer) {
                if(Vector3.Magnitude(playerTransform.position - transform.position) <= thresholdPosition) {
                    catchPlayer = false;
                    animator.applyRootMotion = false;
                    animator.SetBool("Idle", true);
                }
            }
        }

        public void StopFollowingPlayer() {
            transform.parent = null;

            animator.applyRootMotion = true;

            catchPlayer = true;
        }

    }
}