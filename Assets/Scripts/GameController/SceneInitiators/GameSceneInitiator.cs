using UnityEngine;
using System.Collections;
using App.TheValleyChase.UI;
using App.TheValleyChase.ScreenControls;
using UnityEngine.UI;

namespace App.TheValleyChase.GameController.SceneInitiators {

    public class GameSceneInitiator : MonoBehaviour {

        public HUDManager hudManager;
        public ScreenFader screenFader;

        public Light[] lights;

        private LightShadows[] lightShadows;
        private PlayerPrefsManager prefsManager;
        private GameStateManager stateManager;
        private PlayerPrefsManager.PlayerPrefs prefs;

        void Start() {
            GameController controller = GameController.Instance;
            stateManager = controller.GetStateManager();
            prefsManager = controller.GetPrefsManager();

            prefs = prefsManager.GetPrefs();

            stateManager.hudManager = hudManager;
            stateManager.screenFader = screenFader;

            stateManager.Play();

            controller.GetScoreManager().StartCounting();

            stateManager.credits = screenFader.transform.GetChild(0).GetComponentsInChildren<Text>();

            lightShadows = new LightShadows[lights.Length];

            for(int i = 0; i < lights.Length; i++) {
                lightShadows[i] = lights[i].shadows;
                if (!prefs.castShadows) {
                    lights[i].shadows = LightShadows.None;
                }
            }
        }

        public void SetGameShadows(bool val) {
            prefs.castShadows = val;
            prefsManager.SavePrefs(prefs);

            for (int i = 0; i < lights.Length; i++) {
                if(val) {
                    lights[i].shadows = lightShadows[i];
                } else {
                    lights[i].shadows = LightShadows.None;
                }
            }
        }

    }
}