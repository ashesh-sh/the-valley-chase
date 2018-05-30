using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace App.TheValleyChase.GameController {

    public class ScoreManager : MonoBehaviour {

        public bool isScoreCounting;

        private int deltaScore = 1;
        private int localScore = 0;

        private Score scoreObject;
        private const string scoreFile = "/score.dat";

        void Start() {
            if(GameController.Instance.GetStateManager().IsGamePlaying()){
                StartCounting();
            }
        }

        void FixedUpdate() {
            if (isScoreCounting) {
                localScore += deltaScore;
            }
        }

        public void StartCounting() {
            localScore = 0;
            isScoreCounting = true;
        }

        public void StopCounting() {
            isScoreCounting = false;
        }

        public int GetCurrentScore() {
            return localScore;
        }

        public void LoadScore() {
            if(File.Exists(Application.persistentDataPath + scoreFile)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + scoreFile, FileMode.Open);

                scoreObject = (Score) bf.Deserialize(file);
                file.Close();
            } else {
                scoreObject = new Score();
            }
        }

        public void SaveHighScore() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + scoreFile);

            Score score = new Score();
            score.score = localScore;

            scoreObject = score;

            bf.Serialize(file,score);
            file.Close();
        }

        public int GetHighScore() {
            if(scoreObject == null || scoreObject.score == 0) {
                LoadScore();
            }
            return scoreObject.score;
        }

        [Serializable]
        private class Score {
            public int score;
        }

    }
}