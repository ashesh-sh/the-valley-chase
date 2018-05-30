using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using App.TheValleyChase.GameController;
using App.TheValleyChase.Player;

namespace App.TheValleyChase.UI {

    public class HUDManager : MonoBehaviour {

        public Image playerDeadPanel;
        public Image pausePanel;
        public Text scoreText;
        public Text[] highScoreText;

        public Button pauseButton;
        public Button resumeButton;
        public Button[] restartButton;
        public Button[] audioToggleButton;

        public Text scoreTextInDeadPanel;
        public Text scoreTextInPausePanel;

        public Toggle shadowsToggle;

        public Slider staminaSlider;

        public PlayerStamina stamina;

        public GameController.SceneInitiators.GameSceneInitiator sceneInitiator;

        private GameStateManager stateManager;
        private AudioManager audioManager;
        private ScoreManager scoreManager;

        void Awake() {
            scoreManager = GameController.GameController.Instance.GetScoreManager();
            stateManager = GameController.GameController.Instance.GetStateManager();
            audioManager = GameController.GameController.Instance.GetAudioManager();
        }

        void Start() {
            pauseButton.onClick.AddListener(() => stateManager.Pause());
            resumeButton.onClick.AddListener(() => stateManager.Resume());

            foreach (Button btn in restartButton) {
                btn.onClick.AddListener(() => stateManager.Restart());
            }

            foreach (Button btn in audioToggleButton) {
                btn.onClick.AddListener(() => ToggleAudio(btn));
            }

            shadowsToggle.onValueChanged.AddListener((bool val) => ToggleShadows(val));

            UpdateAudioToggleButton();
        }

        private void ToggleShadows(bool val) {
            sceneInitiator.SetGameShadows(val);
        }

        private void ToggleAudio(Button btn) {
            audioManager.ToggleBGM(btn);

            UpdateAudioToggleButton();
        }

        void Update() {
            UpdateScoreText();
            UpdateStaminaSlider();
        }

        private void UpdateAudioToggleButton() {
            foreach(Button btn in audioToggleButton) {
                audioManager.SetBGM(btn, audioManager.canPlayBgm);
            }
        }

        public void ShowPlayerDeadPanel() {
            playerDeadPanel.gameObject.SetActive(true);
            scoreTextInDeadPanel.text = "SCORE: " + scoreManager.GetCurrentScore();
            UpdateHighScoreText();
        }

        public void ChangePlayerDeadPanelText(string newtext) {
            playerDeadPanel.gameObject.transform.GetComponentInChildren<Text>().text = newtext;
        }

        public void ShowPausePanel() {
            pausePanel.gameObject.SetActive(true);
            scoreTextInPausePanel.text = "SCORE: " + scoreManager.GetCurrentScore();
            shadowsToggle.isOn = GameController.GameController.Instance.GetPrefsManager().GetPrefs().castShadows;
            UpdateHighScoreText();
        }

        public void HidePausePanel() {
            pausePanel.gameObject.SetActive(false);
        }

        public void UpdateScoreText() {
            scoreText.text = "" + scoreManager.GetCurrentScore();
        }

        public void UpdateStaminaSlider() {
            staminaSlider.value = stamina.GetStamina();
        }

        public void UpdateHighScoreText() {
            foreach(Text text in highScoreText) {
                text.text = "HIGH SCORE: " + scoreManager.GetHighScore();
            }
        }
    }

}
