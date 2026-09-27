using UnityEngine;
public class PlayerMotor:MonoBehaviour
{
    CharacterController cc; Vector3 vel; public float walk=6,sprint=10,gravity=-24,jump=8;
    void Awake(){cc=GetComponent<CharacterController>();}
    void Update(){
        Vector2 m=MobileInput.Move;
#if UNITY_EDITOR
        if(m.sqrMagnitude<.01f)m=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        if(Input.GetKeyDown(KeyCode.Space))MobileInput.Jump=true;
        if(Input.GetKey(KeyCode.LeftShift))MobileInput.Sprint=true;
#endif
        var cam=Camera.main; Vector3 f=cam?cam.transform.forward:Vector3.forward,r=cam?cam.transform.right:Vector3.right;f.y=r.y=0;f.Normalize();r.Normalize();
        Vector3 d=f*m.y+r*m.x;
        if(d.sqrMagnitude>.01f){transform.forward=Vector3.Slerp(transform.forward,d.normalized,14*Time.deltaTime);cc.Move(d.normalized*(MobileInput.Sprint?sprint:walk)*Time.deltaTime);}
        if(cc.isGrounded&&vel.y<0)vel.y=-2;
        if(MobileInput.Jump&&cc.isGrounded)vel.y=jump;
        MobileInput.Jump=false;MobileInput.Sprint=false;
        vel.y+=gravity*Time.deltaTime;cc.Move(vel*Time.deltaTime);
    }
}
