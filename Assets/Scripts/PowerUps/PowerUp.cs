using UnityEngine;
using System.Collections;

namespace App.TheValleyChase.PowerUps {

    public class PowerUp : MonoBehaviour {
        public float staminaIncreaseAmount = 10f;

        public ParticleSystem powerupPickup;

        public void PickUp() {
            powerupPickup.Play();
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(gameObject, powerupPickup.duration);
        }
    }

}
