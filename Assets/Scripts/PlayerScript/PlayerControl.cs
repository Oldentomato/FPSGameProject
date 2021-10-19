using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//애니메이션을 setbool이 아닌 setinteger로인해 키를땠을시 인식이안되던 버그를 고침
struct Movement
{
    public int forward;
    public int backward;
    public int right;
    public int left;
    public int state;
};



public class PlayerControl : MonoBehaviour
{
    [Header("GameObject")]
    public Transform GetArmTrans;
    public Animator GetArmAni;
    public Transform GetAimingArmTrans;
    //public Transform GetSpineTrans;



    //initializing()로 정보입력
    private CharacterController GetChar;
    private Animator GetAni;
    private PlayerGunFire GetFire;
    private InputManager input;
    private Vector3 MoveVec3;
    private CharacterController GetCharCtrl;
    private Camera GetPlayerCam;
    private UICtrl GetPlayerUI;



    private bool b_CursorLock = true;
    [HideInInspector]
    public bool b_Aiming = false;
    [HideInInspector]
    public int i_Health;

    [Header("PlayerSetting")]
    public float f_OriginalCrouchHeight;
    public float f_CrouchHeight;
    public Vector3 GunAimPos;
    public Vector3 GunNotAimPos;

    [Header("PlayerStat")]
    public float f_CrouchMoveSpeed;
    public float f_RunSpeed;
    public float f_MoveSpeed;
    public float f_RotateSpeed;
    public float f_JumpPower;
    public float f_Gravity;
    public float f_CrouchSpeed;
    public float f_AimSpeed;
    public int i_MaxHealth;

    //[Header("ServerSetting")]
    //[SerializeField]
    //private int i_RoomMaxCount;





    Movement move;


    //점프행동체크
    private bool isJump = false;

    //마우스 카메라 좌표
    private float f_MouseX, f_MouseY;



    void Start()
    {
        Initializing();
    }


    void Update()
    {
        CursorLoad();
        GetFire.AutoReloading();
        MoveAniCheck();
        GetPlayerUI.isMove(CheckKeyInput());//UI 십자선 관리
        Aiming();
        InGround();
        GetFire.MouseInput();


    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (b_CursorLock)
        {
            Movement();
            MouseInputRotate();
        }


    }

    private void CursorLoad()
    {
        if (input.Escape)
        {
            if (!b_CursorLock)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                b_CursorLock = true;

            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                b_CursorLock = false;
            }
        }


    }

    private void Initializing()
    {
        GetChar = GetComponent<CharacterController>();
        GetAni = GetComponent<Animator>();
        GetFire = GetComponentInChildren<PlayerGunFire>();
        input = GetComponent<InputManager>();
        GetCharCtrl = GetComponent<CharacterController>();
        GetPlayerCam = GetComponentInChildren<Camera>();
        GetPlayerUI = GetComponentInChildren<UICtrl>();

        //GetPlayerUI.Initial_GetCtrl(i_MaxHealth);




        i_Health = i_MaxHealth;

        move.forward = 3;
        move.backward = -3;
        move.right = 2;
        move.left = -2;
        move.state = 0;




        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        b_CursorLock = true;

    }



    private void SetLayersRecursively(Transform trans, string name)
    {
        if (trans.gameObject.layer != LayerMask.NameToLayer("PlayerArm"))
        {
            trans.gameObject.layer = LayerMask.NameToLayer(name);
        }


        foreach (Transform child in trans)
        {
            SetLayersRecursively(child, name);
        }
    }


    private void Movement()
    {
        if (GetChar.isGrounded)
        {
            WalkAniCheck();
            RunAniCheck();
            CrouchingCheck();
            MoveVec3 = new Vector3(input.Move, 0, input.Strafe);
            MoveVec3 = transform.TransformDirection(MoveVec3);//벡터를 월드좌표계기준으로 변환
            MoveVec3 = MoveVec3.normalized * IsState();
            if (input.Jump && !isJump)
            {
                isJump = true;
                GetAni.SetTrigger("Jump");
                MoveVec3.y = f_JumpPower;
            }


        }
        MoveVec3.y -= f_Gravity * Time.deltaTime;
        GetChar.Move(MoveVec3 * Time.deltaTime);

        GetAni.SetInteger("Movement", move.state);



    }

    private float IsState()
    {
        if (input.Lshift && !b_Aiming)
        {
            return f_RunSpeed;
        }
        else if (input.Crouching)
        {
            return f_CrouchMoveSpeed;
        }
        else
        {
            return f_MoveSpeed;
        }
    }


    private void MoveAniCheck()
    {
        if (input.Move < 0)
            move.state = move.left;
        if (input.Move > 0)
            move.state = move.right;
        if (input.Strafe < 0)
            move.state = move.backward;
        if (input.Strafe > 0)
            move.state = move.forward;

    }
    private void CrouchingCheck()
    {
        if (input.Crouching)
        {
            GetCharCtrl.height = Mathf.Lerp(GetCharCtrl.height, f_CrouchHeight, Time.deltaTime * f_CrouchSpeed);
            GetAni.SetBool("Crouching", true);
        }
        else
        {
            GetAni.SetBool("Crouching", false);
            float lastHeight = GetCharCtrl.height;
            GetCharCtrl.height = Mathf.Lerp(GetCharCtrl.height, f_OriginalCrouchHeight, Time.deltaTime * f_CrouchSpeed);
            //올라올때 충돌로인해 뚝뚝끊기면서 올라오는것을 방지
            Vector3 tmpPosition = transform.position;
            tmpPosition.y += (GetCharCtrl.height - lastHeight);
            transform.position = tmpPosition;

        }
    }

    public bool CheckKeyInput()
    {
        if (input.Move != 0)
            return true;
        else if (input.Strafe != 0)
            return true;
        else
            return false;
    }



    private void WalkAniCheck()
    {

        if (CheckKeyInput())
        {

            GetArmAni.SetBool("Move", true);
        }

        else
        {
            move.state = 0;
            GetArmAni.SetBool("Move", false);
        }
    }
    private void RunAniCheck()
    {
        if (input.Lshift && !b_Aiming && GetArmAni.GetBool("Move"))
            GetArmAni.SetBool("Run", true);
        else
        {
            GetArmAni.SetBool("Run", false);
        }
    }

    private void Aiming()
    {
        if (Input.GetButton("Fire2"))
        {
            GetPlayerCam.fieldOfView = Mathf.Lerp(GetPlayerCam.fieldOfView, 40f, Time.deltaTime * f_AimSpeed);
            GetAimingArmTrans.localPosition = Vector3.Lerp(GetAimingArmTrans.localPosition, GunAimPos, Time.deltaTime * f_AimSpeed);
            b_Aiming = true;
            GetPlayerUI.HideCrossHair(b_Aiming);
        }
        else
        {
            GetPlayerCam.fieldOfView = Mathf.Lerp(GetPlayerCam.fieldOfView, 60f, Time.deltaTime * f_AimSpeed);
            GetAimingArmTrans.localPosition = Vector3.Lerp(GetAimingArmTrans.localPosition, GunNotAimPos, Time.deltaTime * f_AimSpeed);
            b_Aiming = false;
            GetPlayerUI.HideCrossHair(b_Aiming);
        }

    }


    private void MouseInputRotate()
    {

        float f_InputX = Input.GetAxis("Mouse X");
        float f_InputY = Input.GetAxis("Mouse Y");
        f_MouseX += f_InputX * f_RotateSpeed * Time.deltaTime;
        f_MouseY += f_InputY * f_RotateSpeed * Time.deltaTime;
        f_MouseY = Mathf.Clamp(f_MouseY, -90, 90);

        if (GetFire.b_Fire && !GetFire.b_isReloading)
        {//총기반동
            f_MouseY += GetFire.GetGun.f_Recoil;
            f_MouseX += Random.Range(-0.5f, 0.5f);
        }

        transform.rotation = Quaternion.Euler(0, f_MouseX, 0);
        //GetSpineTrans.rotation = Quaternion.Euler(f_MouseY, 0, 0);
        GetArmTrans.rotation = Quaternion.Euler(-f_MouseY, f_MouseX, 0);
    }

    private void InGround()
    {//땅에닫는순간 상태변경
        if (GetChar.isGrounded)
        {
            isJump = false;
        }
    }

    public void TakeDamage(int i_Dmg)
    {

        i_Health -= i_Dmg;
        GetPlayerUI.i_CurrentHP = i_Health; //쓸데없이 변수하나더있어서 만약실행이잘되면 변수하나없애자


    }

}
