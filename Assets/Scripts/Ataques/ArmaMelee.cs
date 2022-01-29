using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaMelee : Arma
{
    float multiplicador = 1;
    public float velocidade = 1;
    
    void Update()
    {
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a = multiplicador;
        this.GetComponent<SpriteRenderer>().color = c;
        multiplicador -= velocidade * Time.deltaTime;
        if(multiplicador < 0)
            Destroy(this.gameObject);
    }
}
