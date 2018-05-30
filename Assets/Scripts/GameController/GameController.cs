using UnityEngine;
using App.TheValleyChase.Framework;
using System.Collections;
using System;

namespace App.TheValleyChase.GameController {

    public class GameController : MonoBehaviour {

        private AudioManager audioManager;
        private ScoreManager scoreManager;
        private GameStateManager stateManager;
        private PlayerPrefsManager prefsManager;
        private GameProgressManager progressManager;

        private static GameController instance;

        public static GameController Instance {
            get {
                if(instance == null) {
                    instance = GameObject.FindObjectOfType<GameController>();
                }
                GameController[] controllers = GameObject.FindObjectsOfType<GameController>();
                if (controllers.Length > 1) {
                    //Debug.LogError("GameController Component must only be attached to a single gameObject");

                    foreach(GameController controller in controllers) {
                        if(controller != instance) {
                            Destroy(controller.gameObject);
                        }
                    }
                }
                return instance;
            }
        }

        void Awake() {
            DontDestroyOnLoad(gameObject);

            audioManager = Instance.GetComponent<AudioManager>();
            scoreManager = Instance.GetComponent<ScoreManager>();
            stateManager = Instance.GetComponent<GameStateManager>();
            prefsManager = Instance.GetComponent<PlayerPrefsManager>();
            progressManager = Instance.GetComponent<GameProgressManager>();
        }

        public GameStateManager GetStateManager() {
            return stateManager;
        }

        public AudioManager GetAudioManager() {
            return audioManager;
        }

        public ScoreManager GetScoreManager() {
            return scoreManager;
        }

        public PlayerPrefsManager GetPrefsManager() {
            return prefsManager;
        }

        public GameProgressManager GetProgressManager() {
            return progressManager;
        }
    }
}