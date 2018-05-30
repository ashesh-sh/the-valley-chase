using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using App.TheValleyChase.ScreenControls;
using App.TheValleyChase.Framework;

namespace App.TheValleyChase.GameController {

    public class SplashSceneController : MonoBehaviour {

        public float nextSceneAfter;
        public ScreenFader screenFader;

        private bool canLoadMenu;

        void Start() {
            Invoke("LoadMenuScene", nextSceneAfter);
            screenFader.StartScene();
        }

        void Update() {
            if(canLoadMenu && screenFader.HasSceneEnded()) {
                SceneManager.LoadScene(GameLevelInfo.MenuScene);
            }
        }

        void LoadMenuScene() {
            canLoadMenu = true;
            screenFader.EndScene();
        }
    }
}