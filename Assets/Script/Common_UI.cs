using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Common_UI : MonoBehaviour
{
    GameObject character_Hp_bar;
    GameObject character_Mp_bar;
    GameObject character_Exp_bar;
    GameObject Hp_Amount_UI;
    GameObject Mp_Amount_UI;
    GameObject Exp_Ratio_UI;
    GameObject character_Lv_UI;
    GameObject Character_Job_UI;
    GameObject Skill;
    GameObject character;
    int character_Hp;
    int Remaining_Hp;
    int character_Mp;
    int Remaining_Mp;
    int character_Exp;
    public int Current_Exp = 0;
    int character_Lv;

    // Hp 변화량을 그리는 함수
    public void DrawHp(int DeltaHp)
    {
        this.Remaining_Hp -= DeltaHp;
        if (this.Remaining_Hp <= 0)
        {
            this.Remaining_Hp = 0;
        }

        float Hp_Rate = (DeltaHp + 0.0f) / character_Hp;
        this.character_Hp_bar.GetComponent<Image>().fillAmount -= Hp_Rate;
        if (this.character_Hp_bar.GetComponent<Image>().fillAmount <= 0.001f)
        {
            this.character_Hp_bar.GetComponent<Image>().fillAmount = 0.001f;
        }
    }

    // Mp 변화량을 그리는 함수
    public void DrawMp(int DeltaMp)
    {
        this.Remaining_Mp -= DeltaMp;
        if (this.Remaining_Mp <= 0)
        {
            this.Remaining_Mp = 0;
        }

        float Mp_Rate = (DeltaMp + 0.0f) / this.character_Mp;
        this.character_Mp_bar.GetComponent<Image>().fillAmount -= Mp_Rate;
        if (this.character_Mp_bar.GetComponent<Image>().fillAmount <= 0.001f)
        {
            this.character_Mp_bar.GetComponent<Image>().fillAmount = 0.001f;
        }
    }

    // Exp 변화량을 그리는 함수
    void DrawExp(int DeltaExp)
    {
        this.Current_Exp += DeltaExp;
        if (this.Current_Exp <= 0)
        {
            this.Current_Exp = 0;
        }
        else if (this.Current_Exp >= this.character_Exp)
        {
            this.Current_Exp -= this.character_Exp;
            this.character.GetComponent<UserController>().Character_Stat.LevelUp(1);
            this.character_Lv = this.character.GetComponent<UserController>().Character_Stat.Level;
            this.character_Exp = this.character.GetComponent<UserController>().Character_Stat.Exp;
        }

        float Exp_Rate = (this.Current_Exp + 0.0f) / this.character_Exp;
        this.character_Exp_bar.GetComponent<Image>().fillAmount = Exp_Rate;
        if (this.character_Exp_bar.GetComponent<Image>().fillAmount <= 0.001f)
        {
            this.character_Exp_bar.GetComponent<Image>().fillAmount = 0.001f;
        }
    }

    void Start()
    {
        this.character_Hp_bar = GameObject.Find("Character_HP");
        this.character_Mp_bar = GameObject.Find("Character_MP");
        this.character_Exp_bar = GameObject.Find("Character_Exp");
        this.Hp_Amount_UI = GameObject.Find("HP_Amount");
        this.Mp_Amount_UI = GameObject.Find("MP_Amount");
        this.Exp_Ratio_UI = GameObject.Find("EXP_Ratio");
        this.character_Lv_UI = GameObject.Find("Character_Lv");
        this.Character_Job_UI = GameObject.Find("Character_Job");
        this.Skill = GameObject.Find("Skill_block");
        this.Skill.SetActive(false);
        this.character = GameObject.Find("Character");
        this.character_Hp = this.character.GetComponent<UserController>().Character_Stat.Hp;
        this.character_Mp = this.character.GetComponent<UserController>().Character_Stat.Mp;
        this.character_Exp = this.character.GetComponent<UserController>().Character_Stat.Exp;
        this.Remaining_Hp = this.character_Hp;
        this.Remaining_Mp = this.character_Mp;
        this.character_Lv = this.character.GetComponent<UserController>().Character_Stat.Level;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.Skill.SetActive(true);
        }

        if (Input.GetButtonDown("Skill_Close"))
        {
            this.Skill.SetActive(false);
        }
        // 대미지, 회복량에 따라 체력바 변환
        if (this.character.GetComponent<UserController>().DeltaHp != 0)
        {
            this.DrawHp(this.character.GetComponent<UserController>().DeltaHp);
            this.character.GetComponent<UserController>().DeltaHp = 0;
        }

        // 사용량, 회복량에 따라 마나바 변환
        if (this.character.GetComponent<UserController>().DeltaMp != 0)
        {
            this.DrawMp(this.character.GetComponent<UserController>().DeltaMp);
            this.character.GetComponent<UserController>().DeltaMp = 0;
        }

        // 경험치 변화량에 따라 경험치바 변환
        this.DrawExp(this.character.GetComponent<UserController>().DeltaExp);
        this.character.GetComponent<UserController>().DeltaExp = 0;

        // UI 표시
        this.Hp_Amount_UI.GetComponent<Text>().text = Remaining_Hp.ToString() + " / " + character_Hp.ToString();  // 체력량 표시
        this.Mp_Amount_UI.GetComponent<Text>().text = Remaining_Mp.ToString() + " / " + character_Mp.ToString();  // 마나량 표시
        this.Exp_Ratio_UI.GetComponent<Text>().text = string.Format("{0:0.#}", (((this.Current_Exp + 0.0f) / this.character_Exp) * 100)) + " %";  // 경험치 비율 표시
        this.character_Lv_UI.GetComponent<Text>().text = "Lv. " + character_Lv.ToString();  // 레벨 표시
        this.Character_Job_UI.GetComponent<Text>().text = this.character.GetComponent<UserController>().Character_Stat.Job;  // 직업 표시
    }
}
