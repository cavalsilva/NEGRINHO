using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanctuary : Interactable
{
    GameManager gm;
    Candle candle;

    public SfxAudio sfxInterageSanctuary;
    public SfxAudio sfxAproximacaoSanctuary;

    public GameObject positionMae1;
    public GameObject positionMae2;
    public GameObject positionMae3;

    private int indexPosMae = 1;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        candle = FindObjectOfType<Candle>();
    }

    public override void Interact()
    {
        Debug.Log("Interagiu com o sanctuary");

        //SFX Interage
        sfxInterageSanctuary.PlayAudio();

        // Set the game manager checkpoint to this location, so if the player dies he gets back here upon restart
        if (gm != null)
            gm.lastCheckpoint = this.transform.position;
        else
            Debug.LogError("Game Manager não está na cena");

        candle.ResetCandle();
        gm.ApplyCoinsBonus();
    }

    public Vector3 GetPositionMae() {

        Vector3 positionMae = new Vector3 (0,0,0);

        if (indexPosMae == 1){
            positionMae = positionMae1.transform.position;
        }
        else if (indexPosMae == 2){
            positionMae = positionMae2.transform.position;
        }
        else if (indexPosMae == 3) {
            positionMae = positionMae3.transform.position;
        }

        indexPosMae++;

        return positionMae;
    }

    private void OnTriggerEnter(Collider other)
    {
        //SFX if the player entered the trigger, feedback the possibility of interaction 
        sfxAproximacaoSanctuary.PlayAudio();
    }


}
