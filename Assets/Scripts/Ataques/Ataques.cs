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
    public float lastR, lastM, lastD;
    public bool player;
    // Start is called before the first frame update
    void Start()
    {
        personagem = this.GetComponent<Player>();
        player = this.gameObject.tag == "Player";
    }

    public bool AtaqueMelee(bool mirror = false) {
        bool podeAtacar = this.personagem.podeAgir();
        if(podeAtacar && Time.time > lastM + cooldownM) {
            Vector3 spw = spawnMelee;
            spw.x *= (mirror? -1:1);
            
            GameObject go = Instantiate(this.melee, transform.position + spw, transform.rotation);
            go.GetComponent<Arma>().mirror = mirror;
            lastM = Time.time;
            this.personagem.parar(travaM);
            return true;
        }
        return false;
    }

    public bool AtaqueRanged(bool mirror = false) {
        bool podeAtacar = this.personagem.podeAgir();
        if(podeAtacar && Time.time > lastR + cooldownR) {
            Vector3 spw = spawnRanged;
            spw.x *= (mirror? -1:1);
            GameObject go = Instantiate(this.ranged, transform.position + spw, transform.rotation);
            go.GetComponent<Arma>().mirror = mirror;
            lastR = Time.time;
            this.personagem.parar(travaR);
            return true;
        }
        return false;
    }

    public void dash(){
        if(player){
            Player jogador = (Player)this.personagem;
            if(jogador.podeAgir() && Time.time > lastD + cooldownD){
                lastD = Time.time;
                jogador.runDash();
            }
        }
    }
    
}
