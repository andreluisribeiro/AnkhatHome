using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Personagem : MonoBehaviour
{
    public int speed = 10;
    public int jumpForce = 5;
    public float jumpGravity = 1;
    public Transform chaoPrefab;
    private GameObject chaoCriado;
    private bool jumping = false;
    public float jumpDist = 1;
    private int limitador = 0;
    private float tempoParado = 0;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        input();
    }
    void input(){
        if(tempoParado > 0){
            tempoParado -= Time.deltaTime;
        }else{
            checaPulo();
            andar();
        }
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.gravityScale = jumpGravity;
        jumping = true;
    }
    void andar(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 vel = rb.velocity;
        vel.x = h * speed;
        if(!jumping){
            vel.y = Mathf.Round(v-limitador) * speed;
        }
        this.rb.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject obj = other.gameObject;
        if(obj.tag == "chao" && jumping){
            rb.gravityScale = 0;
            jumping = false;
            Vector3 vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;
            Destroy(chaoCriado);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "limitador"){
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

    public void parar(float tempo){
        this.tempoParado = tempo;
    }

    public bool podeAgir(){
        if(tempoParado > 0){
            return false;
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
}
