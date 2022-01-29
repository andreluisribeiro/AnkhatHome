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
        if(Input.GetButtonDown("Jump") && !jumping){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.gravityScale = jumpGravity;
            jumping = true;
            Vector3 pos = transform.position + new Vector3(0,-0.32f);
            chaoCriado = Instantiate(chaoPrefab, pos, Quaternion.identity).gameObject;
        }
        andar();
        
    }
    void andar(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 vel = rb.velocity;
        vel.x = h * speed;
        if(!jumping){
            vel.y = v * speed;
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
}
