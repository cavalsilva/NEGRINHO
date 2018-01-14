using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //o objeto interagível que está ao alcance do personagem
    Interactable interactable;

    SphereCollider col;
    public bool canDrawGizmos = true;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            //Interage se tiver algo para interagir
            if (interactable != null)
                interactable.Interact();
        }
    }

    //Ao entrar na zona de interação do jogador, armazena o objeto interagível na variável interactable 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " interagível");
        interactable = other.GetComponent<Interactable>();
    }

    //Ao sair na zona de interação do jogador, apaga o valor de interactable
    private void OnTriggerExit(Collider other)
    {
        interactable = null;
    }

    //Desenha a zona interagível no Editor da Unity só pra ficar mais fácil de visualizar
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (canDrawGizmos)
            {
                Color color = Color.cyan;
                color.a = 0.5f;
                Gizmos.color = color;
                Gizmos.DrawSphere(transform.position, col.radius);
            }
        }
    }
}
