using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Personagem))]
public class Ataques : MonoBehaviour
{
    public Personagem personagem;
    public GameObject melee, ranged;
    public Vector3 spawnMelee, spawnRanged;
    public float cooldownR, cooldownM, cooldownD;
    public float travaR, travaM, travaD;
    public float lastR, lastM, lastD, atual;
    public bool player;
    // Start is called before the first frame update
    void Start()
    {
        personagem = this.GetComponent<Player>();
        player = this.gameObject.tag == "Player";
    }

    void Update() {
        atual = Time.time;
        if(player) {
            if(Input.GetKey(KeyCode.J)) {
                AtaqueMelee();
            }else if(Input.GetKey(KeyCode.K)) {
                AtaqueRanged();
            }else if(Input.GetButtonDown("Fire1")) {
                dash();
            }
        }
    }

    void AtaqueMelee() {
        bool podeAtacar = this.personagem.podeAgir();
        if(podeAtacar && Time.time > lastM + cooldownM) {
            Instantiate(this.melee, transform.position + spawnMelee, transform.rotation);
            lastM = Time.time;
            this.personagem.parar(travaM);
        }
    }

    
    void AtaqueRanged() {
        bool podeAtacar = this.personagem.podeAgir();
        if(podeAtacar && Time.time > lastR + cooldownR) {
            Instantiate(this.ranged, transform.position + spawnRanged, transform.rotation);
            lastR = Time.time;
            this.personagem.parar(travaR);
        }
    }

    void dash(){
        if(player){
            Player jogador = (Player)this.personagem;
            if(jogador.podeAgir() && Time.time > lastD + cooldownD){
                lastD = Time.time;
                jogador.runDash();
            }
        }
    }
    
}
