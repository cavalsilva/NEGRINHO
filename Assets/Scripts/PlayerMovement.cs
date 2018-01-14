using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 3.0f;

    public GameObject negrinhoFrente;
    public GameObject negrinhoCostas;
    public GameObject negrinhoLado;

    public SkeletonAnimation animationPlayer;
    public SkeletonAnimation animationPlayerCostas;
    public SkeletonAnimation animationPlayerLado;

    public SfxAudio sfxFootstep;

    private float positionX;
    private float positionZ;

    private bool flipped = false;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        negrinhoCostas.SetActive(false);
        negrinhoLado.SetActive(false);
    }

    void Update()
    {
        positionX = Input.GetAxisRaw("Horizontal") * speed;
        positionZ = Input.GetAxisRaw("Vertical") * speed;

        Vector3 movement = new Vector3(positionX, 0, positionZ) * speed;

        rb.velocity = movement;

        //bool playAudio = true;
        //if (positionX == 0 && positionZ == 0)
        //    playAudio = false;

        //if (playAudio)
        //    sfxFootstep.PlayAudio();

        Movement();


        //TODO: Tocar som walk e idle  
    }

    void Movement() {

        if (positionX == 0 && positionZ == 0)
            SetAnimationIdle();
        else if (positionX > 0 && positionZ == 0)
            SetAnimationRight();
        else if (positionX < 0 && positionZ == 0)
            SetAnimationLeft();
        else if (positionX == 0 && positionZ > 0)
            SetAnimationUp();
        else if (positionX == 0 && positionZ < 0)
            SetAnimationDown();
    }


    void SetAnimationIdle() {
        negrinhoFrente.SetActive(true);
        negrinhoCostas.SetActive(false);
        negrinhoLado.SetActive(false);

        animationPlayer.AnimationName = "idle";
        animationPlayerCostas.AnimationName = "";
        animationPlayerLado.AnimationName = "";
    }

    void SetAnimationRight() {
        negrinhoFrente.SetActive(false);
        negrinhoCostas.SetActive(false);
        negrinhoLado.SetActive(true);

        animationPlayer.AnimationName = "";
        animationPlayerCostas.AnimationName = "";
        animationPlayerLado.AnimationName = "run-lado";

        //animationPlayerLado.transform.localRotation = Quaternion.Euler(25, 0, 0);
        flipped = false;
        animationPlayerLado.transform.localScale = new Vector3(Mathf.Abs(animationPlayerLado.transform.localScale.x), animationPlayerLado.transform.localScale.y, animationPlayerLado.transform.localScale.z);
    }

    void SetAnimationLeft() {
        negrinhoFrente.SetActive(false);
        negrinhoCostas.SetActive(false);
        negrinhoLado.SetActive(true);

        animationPlayer.AnimationName = "";
        animationPlayerCostas.AnimationName = "";
        animationPlayerLado.AnimationName = "run-lado";

        //animationPlayerLado.transform.localRotation = Quaternion.Euler(-25, 0, 0);
        if (!flipped) { 
            animationPlayerLado.transform.localScale = new Vector3(animationPlayerLado.transform.localScale.x * -1, animationPlayerLado.transform.localScale.y, animationPlayerLado.transform.localScale.z);
            flipped = true;
        }
    }

    void SetAnimationUp() {
        negrinhoFrente.SetActive(false);
        negrinhoCostas.SetActive(true);
        negrinhoLado.SetActive(false);

        animationPlayer.AnimationName = "";
        animationPlayerCostas.AnimationName = "run-costas";
        animationPlayerLado.AnimationName = "";
    }

    void SetAnimationDown() {
        negrinhoFrente.SetActive(true);
        negrinhoCostas.SetActive(false);
        negrinhoLado.SetActive(false);

        animationPlayer.AnimationName = "run";
        animationPlayerCostas.AnimationName = "";
        animationPlayerLado.AnimationName = "";
    }

}
