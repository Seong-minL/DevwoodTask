using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1_UI_Director : MonoBehaviour
{
    GameObject MoveTutorial;
    GameObject PortalTutorial;
    GameObject character;

    void Start()
    {
        this.MoveTutorial = GameObject.Find("MoveTutorial");
        this.PortalTutorial = GameObject.Find("PortalTutorial");
        this.character = GameObject.Find("Character");
    }

    void Update()
    {
        if (character.transform.position.x >= 0)
        {
            this.MoveTutorial.SetActive(false);
        }
        else
        {
            this.MoveTutorial.SetActive(true);
        }

        if (character.transform.position.x <= 5.0f)
        {
            this.PortalTutorial.SetActive(false);
        }
        else
        {
            this.PortalTutorial.SetActive(true);
        }
    }
}
