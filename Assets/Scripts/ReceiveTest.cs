using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveTest : MonoBehaviour
{
    // Start is called before the first frame update
    public void receive(string estado) {
        Debug.Log("RC: " + estado);
        if(estado == "Fim") {
            this.GetComponent<Text>().text = "CLIQUE";
        }
    }
}
