using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public bool mirror = true;
    public float dano = 1;

    protected void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.SendMessage("dano", this.dano);
    }
    
}
