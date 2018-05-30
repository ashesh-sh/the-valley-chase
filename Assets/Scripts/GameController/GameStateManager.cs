using App.TheValleyChase.Framework;
using App.TheValleyChase.Player;
using App.TheValleyChase.ScreenControls;
using App.TheValleyChase.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.TheValleyChase.GameController {
    public class GameStateManager : MonoBehaviour {
        public Sprite playSprite;
        public Sprite pauseSprite;
        public Sprite restartSprite;
        public ScreenFader screenFader;
        public HUDManager hudManager;

        public int creditsShowingTime;
        public Text[] credits;

        private bool gameEnded;

        private GameController controller;
        private ScoreManager scoreManager;

        private bool gamePlaying;
        private bool canLoadMenu;

        private float previousTimeScale;
        private bool creditsEnding;
        private bool menuLoading;

        private bool introLoaded;

        void Awake() {
            controller = GameController.Instance;
        }

        void Start() {
            scoreManager = controller.GetScoreManager();
        }

        void Update() {
            if(gamePlaying && !gameEnded && screenFader.HasSceneEnded()) {
                SceneManager.LoadScene(GameLevelInfo.GameScene);
            }

            if(!menuLoading && gameEnded && screenFader.HasSceneEnded()) {
                menuLoading = true;
                Invoke("LoadMenu", creditsShowingTime);
            }

            if (creditsEnding) {
                FadeCredits();
            }

            if(canLoadMenu) {
                SceneManager.LoadScene(GameLevelInfo.MenuScene);
                canLoadMenu = false;
            }
        }

        private void FadeCredits() {
            foreach (Text text in credits) {
                if(text != null) {
                    text.color = Color.Lerp(text.color, Color.clear, screenFader.fadeSpeed * Time.deltaTime);

                    if (text.color.a <= 0.05) {
                        canLoadMenu = true;
                        creditsEnding = false;
                        break;
                    }
                }
            }
        }

        void LoadMenu() {
            creditsEnding = true;
        }

        public void Play() {
            gamePlaying = true;
            screenFader.StartScene();
        }

        public void Pause() {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            gamePlaying = false;
            hudManager.ShowPausePanel();
        }

        public bool IsGamePlaying() {
            return gamePlaying;
        }

        public void Resume() {
            Time.timeScale = previousTimeScale;
            gamePlaying = true;
            hudManager.HidePausePanel();
        }

        public void Restart() {
            screenFader.EndScene();
            Time.timeScale = 1;
            gamePlaying = true;
        }

        public void DeadState() {
            Time.timeScale = 0;
            gamePlaying = false;
            scoreManager.StopCounting();

            if (scoreManager.GetCurrentScore() > scoreManager.GetHighScore()) {
                scoreManager.SaveHighScore();
            }

            hudManager.ShowPlayerDeadPanel();
            hudManager.ChangePlayerDeadPanelText("AIYAA!!");
        }

        public void OnStaminaRunOut() {
            DeadState();
            hudManager.ChangePlayerDeadPanelText("U'VE RUN OUT OF STAMINA");
        }

        public void GameOver(PlayerMovement player) {
            player.StopMovement();

            Invoke("StartGameOverAnimation", 0.5f);

            StartGameOverAnimation();
        }

        private void StartGameOverAnimation() {
            screenFader.EndScene();
            gameEnded = true;

            screenFader.transform.GetChild(0).gameObject.SetActive(true);
        }

        public bool IsOnGameScene() {
            return SceneManager.GetActiveScene().buildIndex == GameLevelInfo.GameScene;
        }

        public void SetIntroFlag(bool flag) {
            introLoaded = flag;
        }

        public bool IsIntroLoaded() {
            return introLoaded;
        }
    }
}