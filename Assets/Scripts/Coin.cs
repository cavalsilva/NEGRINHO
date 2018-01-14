using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Interactable
{

    GameManager gm;
    public SfxAudio sfxColetarMoeda;

    public enum CoinType
    {
        bronze = 1,
        silver = 5,
        golden = 10
    }
    //Reference do enum que será visualizado externamente
    public CoinType coinType;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public override void Interact()
    {
        Debug.Log("Interagiu com a Moeda");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Negrinho")
        {
            gameObject.SetActive(false);
            Debug.Log("Negrinho coleta a moeda e o seu tipo é " + coinType.ToString());
            SetCoins((int)coinType);

            //SFX da Moeda
            sfxColetarMoeda.PlayAudio();
        }
    }

    void SetCoins(int coin)
    {
        gm.AddCoin(coin);
        gm.colectedCoins.Add(this.gameObject);
    }
}
