using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Ataques))]
public class Player : Personagem
{
    private GameObject[] anubises = new GameObject[100];
    public GameObject cenario;
    public AudioSource somAmbiente;
    public AudioClip som_real;
    public AudioClip som_egito;
    
    public Sprite fundo_material;
    public Sprite fundo_espiritual;
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
    public bool god = false, mirror = false;
    private Animator animator;
    private Ataques ataques;
    private SpriteRenderer spriteRenderer;

    public MusicManager soundManager;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.animator = this.GetComponent<Animator>();
        this.ataques = this.GetComponent<Ataques>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.soundManager = this.GetComponent<MusicManager>();
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(tempoParado <= 0){
            if(dash){
                this.rb.velocity = new Vector2(0,0);
                dash = false;
                imune = false;
            }
            input();
        }
    }
    void input(){
        checaPulo();
        movimento();
        if(Input.GetButtonDown("God")) {
            god = !god;
            if(god){
                cenario.GetComponent<SpriteRenderer>().sprite = fundo_espiritual;
                foreach(GameObject go in anubises)
                    go.SetActive(true);
                anubises = new GameObject[100];
                this.somAmbiente.clip = som_egito;
                this.somAmbiente.Play();
            }else{
                cenario.GetComponent<SpriteRenderer>().sprite = fundo_material;
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Inimigo");
                foreach(GameObject go in gos)
                    go.SetActive(false);
                anubises = gos;
                this.somAmbiente.clip = som_real;
                this.somAmbiente.Play();
            }
            this.animator.SetBool("GodMode", god);
        }
        
        if(god && Input.GetButtonDown("Fire2")) {
            
            if(ataques.AtaqueMelee(this.spriteRenderer.flipX)) {
                this.animator.SetTrigger("Attack");
                this.soundManager.Play("Bastet_Attack");
            }
        }else if(god && Input.GetButtonDown("Fire3")) {
            if(ataques.AtaqueRanged(this.spriteRenderer.flipX)) {
                this.animator.SetTrigger("Cast");
                this.soundManager.Play("Bastet_Cast");
            }
        }else if(Input.GetButtonDown("Fire1")) {
            ataques.dash();
        }
    }
    public override void checkMirror()
    {
        if(this.rb.velocity.x != 0)
            mirror = this.rb.velocity.x < 0;
        this.spriteRenderer.flipX = mirror;
    }
    void movimento(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 vel = this.rb.velocity;
        vel.x = h * velocidade;
        if(!jumping){
            vel.y = (v-(Mathf.Abs(v)*limitador)) * velocidade;
        }
        this.animator.SetFloat("Movement", vel.magnitude);
        this.rb.velocity = vel; 
    }
    public void runDash(){
        Vector3 direction = this.rb.velocity;
        parar(dashTime);
        this.rb.AddForce(direction * velDash, ForceMode2D.Impulse);
        dash = true;
        imune = true;
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
        
        this.soundManager.Play(god? "Bastet_Pulo" : "Tet_Pulo");
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
                imune = false;
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

    public override void morrer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
