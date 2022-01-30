using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaRanged : Arma
{
    public float velocidade = 1;
    private void Start() {
        velocidade *= (mirror?-1:1);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(velocidade*Time.deltaTime, 0, 0);
        if(this.transform.position.x > 20)
            Destroy(this.gameObject);
    }
    
    
}
