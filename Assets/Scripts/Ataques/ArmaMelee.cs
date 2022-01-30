using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaMelee : Arma
{
    float multiplicador = 1;
    public float velocidade = 1;

    private void Start() {
        Vector3 v = this.transform.localScale;
        v.x = this.mirror? -1:1;
        this.transform.localScale = v;
    }
    
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
