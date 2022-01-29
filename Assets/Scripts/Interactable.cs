using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public MaquinaEstados maquinaDeEstados;
    public string message;
    // Start is called before the first frame update
    private void Start() {
        this.maquinaDeEstados = GameObject.Find("MaquinaEstados").GetComponent<MaquinaEstados>();
    }
    public void Interact() {
        this.maquinaDeEstados.Emitir(message);
    }
}
