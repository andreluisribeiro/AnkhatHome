using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaRanged : Arma
{
    public float velocidade = 1;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(velocidade*Time.deltaTime, 0, 0);
        if(this.transform.position.y > 20)
            Destroy(this.gameObject);
    }
}
