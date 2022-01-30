using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    
    public float velocidade;
    public int hp;
    public float fadeSpeed;
    private bool fade = true;
    // Start is called before the first frame update
    void Start()
    {
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a = 0;
        this.GetComponent<SpriteRenderer>().color = c;
    }

    // Update is called once per frame
    void Update()
    {
        if(fade){
            fadeIn();
        }
    }
    void fadeIn(){
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a += 0.3f * Time.deltaTime;
        this.GetComponent<SpriteRenderer>().color = c;
        if(c.a >= 1){
            fade = false;
        }
    }
}
