using UnityEngine;
using System.Collections;
using App.TheValleyChase.UI;
using UnityEngine.UI;
using App.TheValleyChase.ScreenControls;
using UnityEngine.SceneManagement;
using App.TheValleyChase.Framework;

namespace App.TheValleyChase.GameController.SceneInitiators {

    public class MenuSceneInitiator : MonoBehaviour {

        public AudioClip bgMusic;

        public Button audioToggleBtn;
        public Button playBtn;
        public ScreenFader screenFader;

        public Renderer[] playerMesh;

        private GameController controller;

        private bool canLoadGame;

        void Start() {
            controller = GameController.Instance;
            controller.GetAudioManager().bgMusic = bgMusic;

            audioToggleBtn.onClick.AddListener(() => controller.GetAudioManager().ToggleBGM(audioToggleBtn));
            playBtn.onClick.AddListener(() => StartGameLoader());

            screenFader.StartScene();
        }

        void Update() {
            if(canLoadGame) {
                if (screenFader.HasSceneEnded()) {
                    SceneManager.LoadScene(GameLevelInfo.GameLoaderScene);
                }
            }
        }

        void StartGameLoader() {
            canLoadGame = true;
            screenFader.EndScene();
        }
    }
}