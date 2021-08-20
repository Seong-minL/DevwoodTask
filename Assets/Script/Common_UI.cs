using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Common_UI : MonoBehaviour
{
    GameObject character_Hp_bar;
    GameObject character_Mp_bar;
    GameObject Hp_Amount_UI;
    GameObject Mp_Amount_UI;
    GameObject character_Lv_UI;
    GameObject character;
    int character_Hp;
    int Remaining_Hp;
    int character_Mp;
    int Remaining_Mp;
    int character_Lv;

    // 대미지를 입으면 Hp를 감소시키는 함수
    public void DecreaseHp(int Damage)
    {
        this.Remaining_Hp -= Damage;
        if (this.Remaining_Hp <= 0)
        {
            this.Remaining_Hp = 0;
        }

        float Hp_Rate = (Damage + 0.0f) / character_Hp;
        this.character_Hp_bar.GetComponent<Image>().fillAmount -= Hp_Rate;
        if (this.character_Hp_bar.GetComponent<Image>().fillAmount <= 0.001f)
        {
            this.character_Hp_bar.GetComponent<Image>().fillAmount = 0.001f;
        }
    }

    // 마나를 소모하면 Mp를 감소시키는 함수
    public void DecreaseMp(int Cost)
    {
        this.Remaining_Mp -= Cost;
        if (this.Remaining_Mp <= 0)
        {
            this.Remaining_Mp = 0;
        }

        float Mp_Rate = (Cost + 0.0f) / character_Mp;
        this.character_Mp_bar.GetComponent<Image>().fillAmount -= Mp_Rate;
        if (this.character_Mp_bar.GetComponent<Image>().fillAmount <= 0.001f)
        {
            this.character_Mp_bar.GetComponent<Image>().fillAmount = 0.001f;
        }
    }

    void Start()
    {
        this.character_Hp_bar = GameObject.Find("Character_HP");
        this.character_Mp_bar = GameObject.Find("Character_MP");
        this.Hp_Amount_UI = GameObject.Find("HP_Amount");
        this.Mp_Amount_UI = GameObject.Find("MP_Amount");
        this.character_Lv_UI = GameObject.Find("Character_Lv");
        this.character = GameObject.Find("Character");
        this.character_Hp = this.character.GetComponent<UserController>().Hp;
        this.character_Mp = this.character.GetComponent<UserController>().Mp;
        this.Remaining_Hp = this.character_Hp;
        this.Remaining_Mp = this.character_Mp;
        this.character_Lv = this.character.GetComponent<UserController>().Level;
    }

    void Update()
    {
        this.Hp_Amount_UI.GetComponent<Text>().text = Remaining_Hp.ToString() + " / " + character_Hp.ToString();
        this.Mp_Amount_UI.GetComponent<Text>().text = Remaining_Mp.ToString() + " / " + character_Mp.ToString();
        this.character_Lv_UI.GetComponent<Text>().text = "Lv. " + character_Lv.ToString();
    }
}
