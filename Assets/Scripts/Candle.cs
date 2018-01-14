using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Candle : MonoBehaviour
{
    public float time;
    //[HideInInspector]
    public float timer;
    [HideInInspector]
    public float lerpT;

    [HideInInspector]
    public float radius;

    bool canDecreaseCandle = true;

    public PostProcessingProfile ppp;
    BloomModel.Settings bloom;
    public float minBloomIntensity = 0.1f;
    public float maxBloomIntensity = 0.8f;

    float bonusTimer;

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        timer = time;
        bloom = ppp.bloom.settings;
        
    }

    void Update()
    {
        if (canDecreaseCandle)
            timer -= Time.deltaTime;

        lerpT = Mathf.InverseLerp(0, time, timer);

        BloomModel.Settings newBloom = bloom;
        float lerpB = Mathf.InverseLerp(time, time + bonusTimer, timer);

        newBloom.bloom.intensity = Mathf.Lerp(minBloomIntensity, maxBloomIntensity, lerpB);
       
        ppp.bloom.settings = newBloom;
    }

    public void ResetCandle()
    {
        StartCoroutine(ResetOverTime());
    }

    public bool resetingTimer = false;
    IEnumerator ResetOverTime()
    {
        resetingTimer = true;
        canDecreaseCandle = false;
        //Aumenta a vela pro tamanho máximo de novo
        while (timer < time)
        {
            timer += 70 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        resetingTimer = false; canDecreaseCandle = true;
        yield return null;

        //TODO tocar audio
    }

    public IEnumerator CoinsBoostOverTime(float value)
    {
        bonusTimer = value;

        //Espera o ResetOverTime terminar antes de aplicar o bonus de moeda 
        while (resetingTimer)
            yield return new WaitForEndOfFrame();

        //Da um delay de um segundo antes e aplicar o bonus de moeda
        yield return new WaitForSeconds(1f);

        float total = timer + value;

        StartCoroutine(gm.DecreaseCoinCount());
        while (timer < total)
        {
            timer += 150 * Time.deltaTime;

           yield return new WaitForEndOfFrame();
        }

        canDecreaseCandle = true;
        yield return null;
    }

}
