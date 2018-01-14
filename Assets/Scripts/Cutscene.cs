using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

    public SfxAudio backgroundMusic;

	// Use this for initialization
	void Start () {
        backgroundMusic.PlayAudio();
        Invoke("AbrirNovaTela", 23);
	}

    void AbrirNovaTela() {
        SceneController scene = new SceneController();
        //TODO POR AQUI O NOME DA SCENE
        scene.OpenScene("MainGame");
    }
}
