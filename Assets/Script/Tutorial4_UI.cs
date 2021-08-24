using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial4_UI : MonoBehaviour
{
    GameObject AttackTutorial;

    void Start()
    {
        this.AttackTutorial = GameObject.Find("AttackTutorial");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.AttackTutorial.SetActive(false);
        }
    }
}
