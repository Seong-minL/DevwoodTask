using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3_UI : MonoBehaviour
{
    GameObject character;
    GameObject RopeTutorial;

    void Start()
    {
        this.character = GameObject.Find("Character");
        this.RopeTutorial = GameObject.Find("RopeTutorial");
    }

    void Update()
    {
        if (this.character.transform.position.x >= -10.9f && this.character.transform.position.x <= -5.1f)
        {
            this.RopeTutorial.SetActive(true);
        }
        else
        {
            this.RopeTutorial.SetActive(false);
        }
    }
}
