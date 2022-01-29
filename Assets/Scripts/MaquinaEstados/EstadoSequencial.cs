using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoSequencial : Estado
{
    public string[] mensagens;
    public int atual = 0;
    
    public override bool Test()
    {
        return atual >= mensagens.Length;
    }

    protected override void Limpar()
    {
        atual = 0;
    }

    protected override void Verificar(string message)
    {
        Debug.Log(message + "==" +  mensagens[atual]);
        if(message == mensagens[atual]) {
            atual++;
        } else {
            atual = 0;
        }
    }

}
