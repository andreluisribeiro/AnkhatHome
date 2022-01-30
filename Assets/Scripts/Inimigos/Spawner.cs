using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject jogador;
    public GameObject inimigo;
    public Collider2D spawnArea;
    public float spawnTime;
    
    [SerializeField]
    public float[] yPositions = new float[3];
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn(){
        if(jogador.GetComponent<Player>().god){
            Vector2 spawnPosition = new Vector2();
            spawnPosition.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);

            int index = Random.Range(0, yPositions.Length);
            float yPos = yPositions[index];
            spawnPosition.y = yPos;

            GameObject ini = Instantiate(inimigo , spawnPosition, Quaternion.identity);
            ini.GetComponent<Inimigo>().jogador = this.jogador;
        }
        
    }
}
