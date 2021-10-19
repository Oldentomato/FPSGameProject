using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class M4A1_Info : MonoBehaviour, IGunMng
{
	private GameObject BulletHole;
	private Camera GetArmCam;
	private RaycastHit hitinfo;
    

	private int BullMax = 30;
	private float FireRating = 15f;
	private float recoil = 1.5f;
    private int BodyDmg = 35;
    private int HeadDmg = 100;
	
	public int i_BullMax{get{return BullMax;}}
	public float f_FireRating{get{return FireRating;}}
	public float f_Recoil{get{return recoil;}}

	public M4A1_Info(GameObject gameobj, Camera cam){
		BulletHole = gameobj;
		GetArmCam = cam;

	}

	public void Hitted(){
        if (Physics.Raycast(GetArmCam.transform.position, GetArmCam.transform.forward, out hitinfo, 100f))
        {
            if (hitinfo.transform.CompareTag("Wall"))
            {
                var effect = (GameObject)Instantiate(BulletHole, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                //NetworkServer.Spawn(effect);
            }

            if (hitinfo.transform.CompareTag("Player"))
            {

                hitinfo.transform.GetComponent<PlayerControl>().TakeDamage(BodyDmg);

            }
        }

    }



}
