using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public bool mirror = true;
    public float dano = 1;
    public string tagAlvo;
    public float facilidadeAcertar;
    void Start() {
        this.GetComponent<SpriteRenderer>().sortingOrder = (int)transform.position.y*100;
    }
    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == tagAlvo){
            if( Mathf.Abs(transform.position.y-other.transform.position.y) < facilidadeAcertar){
                other.gameObject.SendMessage("dano", this.dano);
                Destroy(this.gameObject);
            }
        }
    }
    
}
