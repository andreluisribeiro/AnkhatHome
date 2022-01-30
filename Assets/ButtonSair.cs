using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSair : MonoBehaviour
{
    public Sprite clicked;
    public AudioClip clip;
    public void Click() {
        this.GetComponent<Image>().sprite = this.clicked;
        
        this.GetComponent<AudioSource>().clip = clip;
        this.GetComponent<AudioSource>().Play();
        StartCoroutine("Change");
    }

    public IEnumerator Change() {
        yield return new WaitForSeconds(.5f);
        Application.Quit();
    }
    
}
