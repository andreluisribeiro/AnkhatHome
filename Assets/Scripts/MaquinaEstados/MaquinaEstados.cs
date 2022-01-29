using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaEstados : MonoBehaviour
{
    public Estado[] estados;
    public GameObject[] objetosRecebedores;
    public string estadoAtual;
    // Start is called before the first frame update
    void Start()
    {
        objetosRecebedores = GameObject.FindGameObjectsWithTag("recebedores");
        estados = this.GetComponents<Estado>();
    }


    public void Emitir(string message) {
        Debug.Log("SM:" + message); 
        foreach (var estado in estados)
        {
            estado.Passo(estadoAtual, message);
            estado.Executar();
        }
        
    }

    public void Transicao(string estadoNovo) {
        Debug.Log("SMT:" + estadoNovo); 
        estadoAtual = estadoNovo;
        foreach (GameObject go in objetosRecebedores)
        {
            go.SendMessage("receive", estadoNovo);
        }
    }
}
