using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaeDeOuro : Interactable {

	public GameObject sanctuary;
	public GameObject spawnObject;
    public SfxAudio sfxMaeOuroInterage;
    public SfxAudio sfxMaeOuroSpawna;

    public Animator teste;
    public Sanctuary positionMaeDeOuroInSanctuary;

    private GameObject spawnedObject;
    private bool isInteract = false;
    private bool isDestroyed = false;
    private bool isWaitActive = false;
    private float speed = 5.0f;


    private void Start() {
        StartCoroutine(Wait());

        teste.StopPlayback();
    }

    void Update(){
        
		if (isInteract && !isDestroyed){
			float step = speed * Time.deltaTime;
            spawnedObject.transform.position = Vector3.MoveTowards(spawnedObject.transform.position, sanctuary.transform.position, step);

            if (spawnedObject.transform.position == sanctuary.transform.position) {
                Destroy(spawnedObject);
                isDestroyed = true;

                //Coloca a mãe de ouro na posição do santuário
                //gameObject.transform.position = positionMaeDeOuroInSanctuary.transform.position;
                gameObject.transform.position = positionMaeDeOuroInSanctuary.GetPositionMae();
                teste.StartPlayback();
            }
        }
	}
	public override void Interact(){

		Debug.Log("Interagiu com a mãe de ouro");

        //SFX Interage
        sfxMaeOuroInterage.PlayAudio();

        if (!isInteract) { 
            spawnedObject = Instantiate(spawnObject, transform.position, gameObject.transform.rotation);
            
            if (!isWaitActive) {
                StartCoroutine(Wait());
            }
            isInteract = true;

            //SFX Spawned
            sfxMaeOuroSpawna.PlayAudio();
        }


    }

    IEnumerator Wait() {

        isWaitActive = true;
        yield return new WaitForSeconds(3.0f);
        isWaitActive = false;
    }


}
