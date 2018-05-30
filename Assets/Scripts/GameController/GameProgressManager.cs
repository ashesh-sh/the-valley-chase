using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace App.TheValleyChase.GameController {

    public class GameProgressManager : MonoBehaviour {

        private GameProgress gameProgress;
        private const string gameProgressFile = "/progress.dat";

        public bool HasRanBefore() {

            if(gameProgress == null) {
                LoadGameProgress();
            }

            return gameProgress.hasRanBefore;
        }

        private void LoadGameProgress() {
            if (File.Exists(Application.persistentDataPath + gameProgressFile)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + gameProgressFile, FileMode.Open);

                gameProgress = (GameProgress) bf.Deserialize(file);
                file.Close();
            } else {
                gameProgress = new GameProgress();
            }
        }

        public void MarkGameRan() {
            GameProgress progress = new GameProgress();
            progress.hasRanBefore = true;

            WriteToProgressFile(progress);
        }

        public void UnMarkGameRan() {
            GameProgress progress = new GameProgress();
            progress.hasRanBefore = false;

            WriteToProgressFile(progress);
        }

        private void WriteToProgressFile(GameProgress progress) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + gameProgressFile);

            gameProgress = progress;

            bf.Serialize(file, progress);
            file.Close();
        }

        [Serializable]
        private class GameProgress {
            public bool hasRanBefore;
        }
    }
}