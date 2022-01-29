using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoNaoSequencial : Estado
{
    public string[] mensagens;
    public bool[] checados;
    public int atual = 0;

    public override void Start() {
        base.Start();
        checados = new bool[mensagens.Length];
        for (int i = 0; i < checados.Length; i++)
        {
            checados[i] = false;
        }
    }
    
    public override bool Test()
    {
        foreach (var ch in checados)
        {
            if(!ch) return false;
        }
        return true;
    }

    protected override void Limpar()
    {
        for (int i = 0; i < checados.Length; i++)
        {
            checados[i] = false;
        }
    }

    protected override void Verificar(string message)
    {
        Debug.Log("State["+this.TAG_ESTADO+"]:" + message); 
        for (int i = 0; i < mensagens.Length; i++)
        {
            if(mensagens[i] == message) {
                checados[i] = true;
                return;
            }
        }
    }

}
