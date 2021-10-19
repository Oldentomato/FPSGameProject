using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{

    public GameObject GetMuzzleArm, GetMuzzleInGame;//플레이어팔시점 이펙트,인게임플레이어 이펙트
    public IGunMng GetGun;//총 데이터 인터페이스

    public int i_BullCount;
    public bool b_isReloading = false;
    public bool b_Fire = false;//UI에 사격상태를 가져오기위함




    private float f_ReloadingTime = 2f;//리로딩시간
    private float f_FullReloadingTime = 3f;//0발 리로딩
    private float nextTimeToFire = 0f;//연사력에서 시간저장용
    private bool b_EffectPlaying = false;//리로딩중일때 발사누르고있으면 이펙트가 안나오는것을 방지


    private RaycastHit hitinfo;
    private Camera GetArmCam;//플레이어팔카메라
    private Animator GetArmAni;//플레이어팔 애니메이션

    //프리팹
    [System.Serializable]
    public class prefabs
    {
        [Header("Prefabs")]
        public GameObject BulletPreGam;
    }
    public prefabs Prefabs;
    [System.Serializable]
    public class spawnpoints
    {
        [Header("SpawnPoints")]
        public Transform GetBullSpawnTrans;
    }
    public spawnpoints SpawnPoints;

    void Awake()
    {
        Initial_Gun_Info();
    }





    private void Initial_Gun_Info()
    {
        GetArmAni = GetComponentInChildren<Animator>();
        GetArmCam = GetComponentInChildren<Camera>();
        GetGun = new M4A1_Info(Prefabs.BulletPreGam, GetArmCam);
        i_BullCount = GetGun.i_BullMax;
        
    }


    
    public void MouseInput()
    {
        if (!b_isReloading)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                if (!b_EffectPlaying)
                {
                    ParticleCtrl("Play");
                    b_EffectPlaying = true;
                }
                
                Shooting();
                nextTimeToFire = Time.time + 1f / GetGun.f_FireRating;
            }
            else b_Fire = false;

            if (Input.GetKeyDown("r") && i_BullCount < GetGun.i_BullMax)
            {
                ParticleCtrl("Stop");
                b_EffectPlaying = false;
                StartCoroutine("ReloadCoroutine", f_ReloadingTime);

            }


        }
        if (Input.GetButtonUp("Fire1"))
        {
            ParticleCtrl("Stop");
            b_EffectPlaying = false;
        }
    }



    public void AutoReloading()
    {
        if (i_BullCount <= 0 && !b_isReloading)
        {
            ParticleCtrl("Stop");
            b_EffectPlaying = false;
            StartCoroutine("ReloadCoroutine",f_FullReloadingTime);
        }

    }

    private void ParticleCtrl(string str)
    {
        if (str == "Play")
        {
            GetMuzzleArm.SetActive(true);
            GetMuzzleInGame.SetActive(true);
        }
        else if (str == "Stop")
        {
            GetMuzzleArm.SetActive(false);
            GetMuzzleInGame.SetActive(false);
        }
    }



    IEnumerator ReloadCoroutine(int i_reloadtime)
    {
        if(i_BullCount==0)
            GetArmAni.SetTrigger("Reloading_1");
        else
            GetArmAni.SetTrigger("Reloading_2");
        b_isReloading = true;

        yield return new WaitForSeconds(i_reloadtime);
        i_BullCount = GetGun.i_BullMax;
        b_isReloading = false;
    }



    void Shooting()
    {
        GetGun.Hitted();
        i_BullCount--;
        b_Fire = true;
        GetArmAni.CrossFadeInFixedTime("Shooting", 0.01f);

    }

}
