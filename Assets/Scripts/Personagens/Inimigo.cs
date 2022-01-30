using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Ataques))]
public class Inimigo : Personagem
{
    public float rangeX;
    public float rangeY; 
    public GameObject jogador;
    public Ataques atk;
    public float fadeSpeed;
    private bool fade = true;
    public float forca = 1;
    public MusicManager soundManager;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        imune = true;
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a = 0;
        this.GetComponent<SpriteRenderer>().color = c;
        this.soundManager = this.GetComponent<MusicManager>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(fade){
            fadeIn();
        } else { 
            atack();
            movement();
        }
    }
    void movement(){ 
        Vector2 vel = this.rb.velocity;
        float x_dist = Mathf.Abs(transform.position.x - jogador.transform.position.x);
        float y_dist = Mathf.Abs(transform.position.y - jogador.transform.position.y);
        double stop_dist = y_dist < rangeY ? rangeX : 0.1;
        if(x_dist > stop_dist){
            vel.x = jogador.transform.position.x < this.transform.position.x ? -velocidade : velocidade;
        }else{
            vel.x = 0;
        }
        
        this.rb.velocity = vel;
    }
    public override void checkMirror(){
        float x_dist = Mathf.Abs(transform.position.x - jogador.transform.position.x);
        if(x_dist > 0.1){
            bool mirror = transform.position.x < jogador.transform.position.x;
            this.GetComponent<SpriteRenderer>().flipX = mirror;
        }
    }
    void atack(){
        float x_dist = Mathf.Abs(transform.position.x - jogador.transform.position.x);
        float y_dist = Mathf.Abs(transform.position.y - jogador.transform.position.y);
        bool mirror = transform.position.x > jogador.transform.position.x;
        if(x_dist < rangeX && y_dist < rangeY){
            if(atk.AtaqueMelee(mirror)){
                this.GetComponent<Animator>().SetTrigger("Attack");
                this.soundManager.Play("Attack");
            }
        }
    }
    void fadeIn(){
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a += 0.3f * Time.deltaTime;
        this.GetComponent<SpriteRenderer>().color = c;
        if(c.a >= 1){
            fade = false;
            imune = false;
        }
    }

    public override void morrer()
    {
        Destroy(this.gameObject);
    }

    

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.SendMessage("dano", this.forca);
        }
    }
}
