using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Personagem))]
public class Ataques : MonoBehaviour
{
    public Personagem personagem;
    public GameObject melee, ranged;
    public Vector3 spawnMelee, spawnRanged;
    public float cooldownR, cooldownM;
    public float lastR, lastM, atual;
    public bool player;
    // Start is called before the first frame update
    void Start()
    {
        personagem = this.GetComponent<Personagem>(); 
        player = this.gameObject.tag == "Player";
    }

    void Update() {
        atual = Time.time;
        if(player) {
            if(Input.GetKey(KeyCode.J)) {
                Debug.Log("J");
                AtaqueMelee();
            }
            else if(Input.GetKey(KeyCode.K)) {
                AtaqueRanged();
            }
        }
    }

    void AtaqueMelee() {
        bool podeAtacar = true;
        if(podeAtacar && Time.time > lastM + cooldownM) {
            Instantiate(this.melee, transform.position + spawnMelee, transform.rotation);
            lastM = Time.time;
        }
    }

    
    void AtaqueRanged() {
        bool podeAtacar = true;
        Debug.Log(""+(Time.time + cooldownR) + ", " + lastR);
        if(podeAtacar && Time.time > lastR + cooldownR) {
            Instantiate(this.ranged, transform.position + spawnRanged, transform.rotation);
            lastR = Time.time;
        }
    }

    
}
