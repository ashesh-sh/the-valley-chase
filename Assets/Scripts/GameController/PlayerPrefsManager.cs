using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace App.TheValleyChase.GameController {

    public class PlayerPrefsManager : MonoBehaviour {

        private PlayerPrefs prefs;
        private const string prefsFile = "/prefs.dat";

        public void SavePrefs(PlayerPrefs prefs) {
            this.prefs = prefs;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + prefsFile);

            bf.Serialize(file, this.prefs);
            file.Close();
        }


        public PlayerPrefs GetPrefs() {
            if(prefs == null) {
                LoadPrefs();
            }
            return prefs;
        }

        private void LoadPrefs() {
            if (File.Exists(Application.persistentDataPath + prefsFile)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + prefsFile, FileMode.Open);
                prefs = (PlayerPrefs) bf.Deserialize(file);
                file.Close();
            } else {
                prefs = new PlayerPrefs();
                prefs.castShadows = true;
            }
        }

        [Serializable]
        public class PlayerPrefs {
            public bool castShadows;
        }
    }
}