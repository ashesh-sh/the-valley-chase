using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using App.TheValleyChase.ScreenControls;

namespace App.TheValleyChase.SceneInitiators {

    public class GameLoaderInitiator : MonoBehaviour {
        public ScreenFader screenFader;

        public Text commentsText;
        public float commentsTimer;

        public string[] comments;

        void Awake() {
            GameController.GameController.Instance.GetStateManager().screenFader = screenFader;
        }

        void Start() {
            InvokeRepeating("ChangeCommentsText", 0f, commentsTimer);
            screenFader.StartScene();
        }

        void ChangeCommentsText() {
            commentsText.text = comments[Random.Range(0, comments.Length)];
        }
    }
}