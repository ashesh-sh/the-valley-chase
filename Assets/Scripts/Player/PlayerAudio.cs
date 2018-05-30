using UnityEngine;
using System.Collections;

namespace App.TheValleyChase.Player {

    public class PlayerAudio : MonoBehaviour {

        public AudioClip aiyaaClip;
        public AudioClip jumpClip;
        public AudioClip redbullPickupClip;

        private bool deadPlayed;

        private AudioSource audioSource;

        void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayDeadClip() {
            if (!deadPlayed) {
                if (audioSource.isPlaying) {
                    audioSource.Stop();
                }

                audioSource.clip = aiyaaClip;
                audioSource.Play();

                deadPlayed = true;
            }
        }

        public void PlayJumpClip() {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }

            audioSource.clip = jumpClip;
            audioSource.Play();
        }

        public void PlayPickupClip() {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }

            audioSource.clip = redbullPickupClip;
            audioSource.Play();
        }
    }
}