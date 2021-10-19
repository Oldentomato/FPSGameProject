using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Input Mappings
    private string rotateX = "Mouse X";
    private string rotateY = "Mouse Y";
    private string move = "Horizontal";
    private string strafe = "Vertical";
    private string jump = "Jump";
    private string lshift = "Fire3";
    private string crouching = "left ctrl";
    private string escape = "Cancel";

    public float RotateX { get { return Input.GetAxisRaw(rotateX); } }
    public float RotateY { get { return Input.GetAxisRaw(rotateY); } }
    public float Move { get { return Input.GetAxisRaw(move); } }//Raw는 -1,0,1만 반환하여 반응속도가 조금더빠름 대신 부드러운 움직임은 x
    public float Strafe { get { return Input.GetAxisRaw(strafe); } }
    public bool Jump { get { return Input.GetButtonDown(jump); } }
    public bool Lshift { get { return Input.GetButton(lshift); } }
    public bool LshiftDown { get { return Input.GetButtonDown(lshift); } }
    public bool Crouching { get { return Input.GetKey(crouching); } }
    public bool Num_1 { get { return Input.GetKey(KeyCode.Alpha1); } }
    public bool Escape { get { return Input.GetButtonDown(escape); } }

}
