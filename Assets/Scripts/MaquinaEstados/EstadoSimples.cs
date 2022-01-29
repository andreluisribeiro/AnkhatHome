using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoSimples : Estado
{
    public string mensagem;
    public bool completado = false;
    
    public override bool Test()
    {
        return completado;
    }

    protected override void Limpar()
    {
        completado = false;
    }

    protected override void Verificar(string message)
    {
        if(message == mensagem) {
            completado = true;
        } 
    }

}
