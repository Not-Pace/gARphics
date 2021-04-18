    using System.Collections;  
    using System.Collections.Generic;  
    using UnityEngine;  
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;  
    using System.IO;

    
    public class SceneChanger: MonoBehaviour {  
        public void NextScene(string Scene) {  
            if(Scene == "Scene3"){
                #if UNITY_ANDROID
                    string path = "file:///"+ Application.persistentDataPath + "/Data.csv";
                #else 
                    string path = Path.Combine( Application.persistentDataPath, "Data.csv");
                #endif
                Globals.pointList = CSVReader.Read(path);
                Globals.column1 = int.Parse(GameObject.Find("/Canvas/InputX/Text").GetComponent<Text>().text);
                Globals.column2 = int.Parse(GameObject.Find("/Canvas/InputY/Text").GetComponent<Text>().text);
                Globals.column3 = int.Parse(GameObject.Find("/Canvas/InputZ/Text").GetComponent<Text>().text);
            
            }
            SceneManager.LoadScene(Scene);  
      
    }   
}