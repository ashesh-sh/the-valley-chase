using UnityEngine;
using UnityEngine.UI;

namespace App.TheValleyChase.GameController {
    public class AudioManager : MonoBehaviour{

        public bool canPlayBgm;

        public AudioClip bgMusic;
        public Sprite volumeUpImage;
        public Sprite volumeMuteImage;

        private AudioSource audioSource;

        private float maxVolume;

        void Awake() {
            canPlayBgm = true;
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            maxVolume = audioSource.volume;
        }

        void Update() {
            if (canPlayBgm) {
                if (!audioSource.isPlaying) {
                    audioSource.clip = bgMusic;
                    audioSource.loop = true;
                    audioSource.Play();
                } else {
                    audioSource.volume = maxVolume;
                }
            } else if(!canPlayBgm && audioSource.isPlaying){
                audioSource.volume = 0f;
            }
        }

        public void ToggleBGM(Button btn) {
            SetBGM(btn, !canPlayBgm);
        }

        public void SetBGM(Button btn,bool canPlay) {
            canPlayBgm = canPlay;

            if (canPlayBgm) {
                btn.image.sprite = volumeUpImage;
            } else {
                btn.image.sprite = volumeMuteImage;
            }
        }
    }
}