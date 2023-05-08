using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string scene;
    
    public void LoadGame() {
        //Load the gameplay scene
        SceneManager.LoadScene(scene);
    }

}
