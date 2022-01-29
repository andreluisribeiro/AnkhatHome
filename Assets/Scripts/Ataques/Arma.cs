using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public float dano = 1;
    private void OnCollisionEnter(Collision other) {
        Personagem p;
        if(other.gameObject.TryGetComponent<Personagem>(out p)) {
            // TODO dano
        }
    }
}
