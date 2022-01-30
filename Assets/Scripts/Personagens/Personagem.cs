using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Personagem : MonoBehaviour
{
    public float velocidade = 10;
    public float tempoParado = 0;
    public float hp = 5;
    protected Rigidbody2D rb;
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    public virtual void Update()
    {
        if(tempoParado > 0){
            tempoParado -= Time.deltaTime;
        }
    }
    public void parar(float tempo){
        this.rb.velocity = new Vector2(0,0);
        this.tempoParado = tempo;
    }
    public virtual bool podeAgir(){
        return tempoParado > 0;
    }

    public void dano(float d) {
        Debug.Log("Dano: " + this.gameObject.name);
        hp = (hp - d < 0) ? 0 : hp - d;
    }

    
}
