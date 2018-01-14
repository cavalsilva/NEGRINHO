using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public void OpenScene(string nameScene) {
        SceneManager.LoadScene(nameScene);
    }

    private void Update(){
        if (Input.GetKeyDown("Interact")) {
            SceneManager.LoadScene("Cutscene");
        }
    }

}
