using UnityEngine;
using App.TheValleyChase.Framework;
using System;
using App.TheValleyChase.UI;
using App.TheValleyChase.PowerUps;
using App.TheValleyChase.GameControls;
using App.TheValleyChase.Enemy;
namespace App.TheValleyChase.Player {

    public class PlayerCollision : MonoBehaviour {

        private PlayerMovement movementController;
        private PoliceMovement policeMovement;
        private PlayerStamina playerStamina;

        private PlayerAudio playerAudio;

        void Awake() {
            movementController = GetComponent<PlayerMovement>();
            policeMovement = GetComponentInChildren<PoliceMovement>();

            playerStamina = GetComponent<PlayerStamina>();
            playerAudio = GetComponent<PlayerAudio>();
        }

        void OnCollisionEnter(Collision col) {
            if (col.gameObject.tag == TagsContainer.Obstacle) {
                DieConditionally(col);
            }
        }

        void OnTriggerEnter(Collider col) {
            if (col.gameObject.tag == TagsContainer.TurnTrigger) {
                movementController.EnableTurning(col.gameObject.GetComponent<TurnTrigger>());
            } else if(col.gameObject.tag == TagsContainer.PowerUp) {
                PickUpPowerUp(col);
            } else if (col.gameObject.tag == TagsContainer.GameOverTrigger) {
                GameController.GameController.Instance.GetStateManager().GameOver(movementController);
            }
        }

        private void PickUpPowerUp(Collider col) {
            PowerUp powerUp = col.gameObject.GetComponent<PowerUp>();
            playerStamina.IncreaseStamina(powerUp);
            powerUp.PickUp();
            playerAudio.PlayPickupClip();
        }

        void OnTriggerExit(Collider col) {
            if(col.gameObject.tag == TagsContainer.TurnTrigger) {
                movementController.DisableTurning();
            }
        }

        private void DieConditionally(Collision col) {
            Vector3 contactNormal = col.contacts[0].normal;

            float angle = Mathf.Round(Vector3.Angle(transform.forward, contactNormal));

            float collisonAngle = 180f;
            float collisonAngleRounder = 5f;

            float forwardVelocity = Mathf.Abs(Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.forward));

            if ((angle <= (collisonAngle + collisonAngleRounder) && angle >= (collisonAngle - collisonAngleRounder)) || forwardVelocity <= 1f) {
                Die();
            }
        }

        private void Die() {
            Invoke("SetDeadState", 1f);
            movementController.SetDead(true);
            playerAudio.PlayDeadClip();
            policeMovement.StopFollowingPlayer();
        }

        private void SetDeadState() {
            GameController.GameController gameController = GameController.GameController.Instance;
            gameController.GetStateManager().DeadState();
        }
    }
}
