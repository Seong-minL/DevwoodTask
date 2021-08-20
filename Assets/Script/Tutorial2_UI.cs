using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial2_UI : MonoBehaviour
{
    GameObject JumpTutorial;
    GameObject character;

    void Start()
    {
        this.JumpTutorial = GameObject.Find("JumpTutorial");
        this.character = GameObject.Find("Character");
    }

    void Update()
    {
        if (character.transform.position.x >= 0 && character.transform.position.x <= 9.0f)
        {
            JumpTutorial.SetActive(true);
        }
        else
        {
            JumpTutorial.SetActive(false);
        }
    }
}
