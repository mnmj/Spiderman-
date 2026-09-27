using UnityEngine;
public class CameraRig:MonoBehaviour
{
    public Transform target; float yaw=0,pitch=12; public float distance=6,height=2.4f;
    void LateUpdate(){
        if(!target)return;
#if UNITY_EDITOR
        yaw+=Input.GetAxis("Mouse X")*3; pitch-=Input.GetAxis("Mouse Y")*3;
#endif
        pitch=Mathf.Clamp(pitch,-10,55);
        Quaternion q=Quaternion.Euler(pitch,yaw,0);
        Vector3 desired=target.position+q*new Vector3(0,height,-distance);
        transform.position=Vector3.Lerp(transform.position,desired,1-Mathf.Exp(-12*Time.deltaTime));
        transform.LookAt(target.position+Vector3.up*1.3f);
    }
}
