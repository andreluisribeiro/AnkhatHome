using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Personagem
{
    public int jumpForce = 5;
    public float jumpGravity = 1;
    public Transform chaoPrefab;
    public float velDash = 10;
    public float dashTime = 1;
    private GameObject chaoCriado;
    private bool jumping = false;
    public float jumpDist = 1;
    private int limitador = 0;
    private bool dash = false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(tempoParado <= 0){
            if(dash){
                this.rb.velocity = new Vector2(0,0);
                dash = false;
            }
            input();
        }
    }
    void input(){
        checaPulo();
        movimento();
    }
    void movimento(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 vel = this.rb.velocity;
        vel.x = h * velocidade;
        if(!jumping){
            vel.y = (v-(Mathf.Abs(v)*limitador)) * velocidade;
        }
        this.rb.velocity = vel; 
    }
    public void runDash(){
        Vector3 direction = this.rb.velocity;
        parar(dashTime);
        this.rb.AddForce(direction * velDash, ForceMode2D.Impulse);
        dash = true;
    }
    void checaPulo(){
        if(Input.GetButtonDown("Jump")){
            if(!jumping){
                pulo();
                Vector3 pos = transform.position + new Vector3(0,-0.32f);
                chaoCriado = Instantiate(chaoPrefab, pos, Quaternion.identity).gameObject;
            }else{
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                if (hit.collider.tag == "plataforma" && distance <= jumpDist){
                    pulo();
                }
            }
        }
    }
    void pulo(){
        this.rb.velocity = new Vector2(0,0);
        this.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        this.rb.gravityScale = jumpGravity;
        jumping = true;
    }

    public override bool podeAgir(){
        if(base.podeAgir()){
            return true;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        float distance = Mathf.Abs(hit.point.y - transform.position.y);
        bool estaPlataforma = distance < jumpDist;
        if(!jumping || estaPlataforma){
            return true;
        }else{
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject obj = other.gameObject;
        if(obj.tag == "chao" && jumping){
            this.rb.gravityScale = 0;
            jumping = false;
            Vector3 vel = this.rb.velocity;
            vel.y = 0;
            this.rb.velocity = vel;
            Destroy(chaoCriado);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "limitador"){
            if(dash){
                this.rb.velocity = new Vector2(0,0);
                dash = false;
            }
            if(transform.position.y > other.transform.position.y){
                limitador = -1;
            }else{
                limitador = 1;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "limitador"){
            limitador = 0;
        }
    }
}