using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Estado : MonoBehaviour
{
    public MaquinaEstados maquinaDeEstados;
    public string TAG_ESTADO;
    public string[] estadosOuvir;
    public virtual void Start() {
        this.maquinaDeEstados = GameObject.Find("MaquinaEstados").GetComponent<MaquinaEstados>();
    }
    public void Executar()
    {
        if(this.Test()) {
            this.maquinaDeEstados.Transicao(this.TAG_ESTADO);
            this.Limpar();
        }
    }
    public void Passo(string estadoAtual, string message) {
        foreach (var eo in estadosOuvir)
        {
            Debug.Log("S["+TAG_ESTADO+"] => " + eo + ","+ estadoAtual);
            if(eo == estadoAtual) {
                this.Verificar(message);
                break;
            }
        }
    }
    public abstract bool Test();
    protected abstract void Verificar(string message);
    protected abstract void Limpar();
}
