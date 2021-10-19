using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    //public Transform InstanTrans;
    //public Transform Instan_m4a1;
    public GameObject[] GetWeapons;//나중에 외부에서 instan으로 정보들을 받음
    public InputManager input;

    private float f_SwitchDelay = 1f;
    private bool b_Switching = false;
    private int index = 0;



    void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        for (int i = 49; i < 58; i++)
        {//이부분 나중에 검색
            if (Input.GetKeyDown((KeyCode)i) && !b_Switching && GetWeapons.Length > i - 49 && index != i - 49)
            {
                index = i - 49;
                StartCoroutine(SwitchDelay(index));
            }
        }
    }
    private void SwapWeapon(int newindex)
    {
        for (int i = 0; i < GetWeapons.Length; i++)
        {
            GetWeapons[i].SetActive(false);
        }
        GetWeapons[newindex].SetActive(true);
    }

    private IEnumerator SwitchDelay(int newindex)
    {
        b_Switching = true;
        SwapWeapon(newindex);
        yield return new WaitForSeconds(f_SwitchDelay);
        b_Switching = false;
    }

}
