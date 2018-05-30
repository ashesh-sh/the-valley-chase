using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using App.TheValleyChase.Framework;
using App.TheValleyChase.ScreenControls;

namespace App.TheValleyChase.GameController {

    public class GameLoader : MonoBehaviour {

        public ScreenFader screenFader;

        private AsyncOperation async;

        private bool canActivate;
        private bool isSceneLoaded;
        private GameStateManager stateManager;

        void Start() {
            stateManager = GameController.Instance.GetStateManager();
            Invoke("StartLoading", 0.5f);
        }

        void Update() { 
            if (async != null && !isSceneLoaded) {
                if(Mathf.Approximately(async.progress,0.9f)) {
                    isSceneLoaded = true;
                    canActivate = true;
                    screenFader.EndScene();
                }
            }

            if (canActivate && screenFader.HasSceneEnded()) {
                canActivate = false;
                ActivateScene();
            }
        }

        private IEnumerator LoadGameIntro() {
            async = SceneManager.LoadSceneAsync(GameLevelInfo.GameIntroScene);
            async.allowSceneActivation = false;

            yield return async;
        }

        private IEnumerator LoadGame() {
            async = SceneManager.LoadSceneAsync(GameLevelInfo.GameScene);
            async.allowSceneActivation = false;

            yield return async;
        }

        private void ActivateScene() {
            async.allowSceneActivation = true;
        }

        private void StartLoading() {
            if (stateManager.IsIntroLoaded()) {
                StartCoroutine(LoadGame());
            } else {
                StartCoroutine(LoadGameIntro());
            }
        }
    }
}