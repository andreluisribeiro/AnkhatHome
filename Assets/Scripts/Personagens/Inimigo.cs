using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inimigo : Personagem
{
    public GameObject jogador;
    public float fadeSpeed;
    private bool fade = true;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a = 0;
        this.GetComponent<SpriteRenderer>().color = c;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(fade){
            fadeIn();
        } else { 
            movement();
        }
    }
    void movement(){ 
        Vector2 vel = this.rb.velocity;
        vel.x = jogador.transform.position.x < this.transform.position.x ? -velocidade : velocidade;
        this.rb.velocity = vel;
    }
    void fadeIn(){
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a += 0.3f * Time.deltaTime;
        this.GetComponent<SpriteRenderer>().color = c;
        if(c.a >= 1){
            fade = false;
        }
    }
}
