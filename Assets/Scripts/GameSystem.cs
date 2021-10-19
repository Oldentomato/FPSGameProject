using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameSystem : MonoBehaviour
{

    void Start()
    {
        //MouseInitial();
        //OnStartServer();
    }

	private void MouseInitial(){
		Cursor.visible = false;	
		Cursor.lockState = CursorLockMode.Locked;
	}

    /*public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Start_Server J");
        StartServerCamSetting();
        base.OnServerAddPlayer(conn, playerControllerId);
    }*/


}
