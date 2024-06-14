using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ClickButton(int numberButton)
    {
        audio.Play();
        Application.LoadLevel(numberButton);
    }
}
