using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public PlayerGunFire GetFire;//gameobject로 받고 getcomponent로 하기
                                 //public PlayerControl GetCtrl;

    public GameObject GetCrossHair;
    public Text GetBullMaxTxt, GetBullCurrentTxt;

    
    public Text GetPlayerHP;
    private Animator GetCrossHairAni;


    public int i_CurrentHP;



    void Start()
    {
        Initial();
    }
    void Update()
    {
        //isMove();
        isShooting();
        //HideCrossHair();
        SetBullCount();


    }

    //public void Initial_GetCtrl(int i_Health)
    //{
    //    i_TextHealth = i_Health;
    //}



    private void Initial()
    {
        GetCrossHairAni = GetComponentInChildren<Animator>();
        GetFire.i_BullCount = GetFire.GetGun.i_BullMax;
        GetBullMaxTxt.text = GetFire.GetGun.i_BullMax.ToString();
        GetBullCurrentTxt.text = GetFire.i_BullCount.ToString();


        
    }

    private void SetBullCount()
    {
        GetBullCurrentTxt.text = GetFire.i_BullCount.ToString();

    }


    public void HideCrossHair(bool b_isAim)
    {
        GetCrossHair.SetActive(!b_isAim);
    }

    public void isMove(bool b_isMove)
    {
        GetCrossHairAni.SetBool("Move", b_isMove);
    }

    private void isShooting()
    {
        if (GetFire.b_Fire)
        {
            GetCrossHairAni.CrossFadeInFixedTime("Crosshair_FireAni", 0.01f);

        }
    }


    void WriteHP(int i_CurrentHP,int i_CurrentHP_2)
    {
        GetPlayerHP.text = i_CurrentHP.ToString();
    }


}
