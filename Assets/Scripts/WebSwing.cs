using UnityEngine;
public class WebSwing:MonoBehaviour
{
    SpringJoint joint; LineRenderer line; Vector3 anchor; float cd;
    void Awake(){line=gameObject.AddComponent<LineRenderer>();line.positionCount=2;line.startWidth=.035f;line.endWidth=.02f;line.material=new Material(Shader.Find("Sprites/Default"));line.startColor=line.endColor=Color.white;line.enabled=false;}
    void Update(){
        if(cd>0)cd-=Time.deltaTime;
        if(MobileInput.Swing&&joint==null&&cd<=0)Grapple();
        MobileInput.Swing=false;
        if(joint){line.enabled=true;line.SetPosition(0,transform.position+Vector3.up*1.4f);line.SetPosition(1,anchor);}
    }
    void Grapple(){
        Camera cam=Camera.main;if(!cam)return;
        Ray ray=new Ray(cam.transform.position,cam.transform.forward);
        if(Physics.Raycast(ray,out var hit,55)){anchor=hit.point;joint=gameObject.AddComponent<SpringJoint>();joint.autoConfigureConnectedAnchor=false;joint.connectedAnchor=anchor;float d=Vector3.Distance(transform.position,anchor);joint.maxDistance=d*.82f;joint.minDistance=d*.22f;joint.spring=8;joint.damper=2;joint.massScale=4;}
    }
    public void Release(){if(joint)Destroy(joint);line.enabled=false;cd=.2f;}
}
