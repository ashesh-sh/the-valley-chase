using UnityEngine;
using System.Collections;
using App.TheValleyChase.ScreenControls;
using System;
using App.TheValleyChase.Cameras;
using UnityEngine.SceneManagement;
using App.TheValleyChase.Framework;
using UnityEngine.UI;

namespace App.TheValleyChase.GameController.SceneInitiators {

    public class GameIntroInitiator : MonoBehaviour {

        public float startTimer;
        public float textIntroTimer;
        public float textIntroSpeed;
        public float textPositionThreshod;

        public GameIntroCameraMovement cameraMovement;
        public ScreenFader screenFader;
        public AudioClip sirenClip;
        public RectTransform introText;

        private bool hasIntroStarted;
        private bool hasScreenEndStarted;
        private bool isTextAnimating;

        private Vector3 originalTextPosition;
        private AudioSource audioSource;

        void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        void Start() {
            screenFader.StartScene();

            originalTextPosition = introText.anchoredPosition3D;

            introText.anchoredPosition3D = new Vector3(Mathf.Abs(introText.anchoredPosition3D.x),introText.anchoredPosition3D.y,introText.anchoredPosition3D.z);

            audioSource.clip = sirenClip;
            audioSource.Play();

            GameController.Instance.GetStateManager().SetIntroFlag(true);
        }

        void Update() {
            if (!hasIntroStarted && screenFader.HasSceneStarted()) {
                Invoke("StartIntro", startTimer);
                hasIntroStarted = true;
            }

            if(!hasScreenEndStarted && cameraMovement.HasIntroEnded()) {
                hasScreenEndStarted = true;
                screenFader.EndScene();
            }

            if (cameraMovement.HasIntroEnded()) {
                FadeSiren();
            }

            if(hasScreenEndStarted && screenFader.HasSceneEnded()) {
                SceneManager.LoadScene(GameLevelInfo.GameLoaderScene);
            }

            if (isTextAnimating) {
                introText.anchoredPosition3D = Vector3.Lerp(introText.anchoredPosition3D, originalTextPosition, Time.deltaTime * textIntroSpeed);

                if(Vector3.Magnitude(introText.anchoredPosition3D - originalTextPosition) <= textPositionThreshod){
                    isTextAnimating = false;
                }
            }
        }

        void StartIntro() {
            cameraMovement.StartIntro();
            Invoke("StartTextIntro", textIntroTimer);
        }

        void StartTextIntro() {
            isTextAnimating = true;
        }

        void FadeSiren() {
            if (audioSource.isPlaying) {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 4f);

                if(audioSource.volume <= 0.1f) {
                    audioSource.volume = 0f;
                    audioSource.Stop();
                }
            }
        }
    }
}