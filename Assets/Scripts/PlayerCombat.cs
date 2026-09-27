using UnityEngine;
public class PlayerCombat:MonoBehaviour
{
    float cd;
    void Update(){
        if(cd>0)cd-=Time.deltaTime;
        if(MobileInput.Attack&&cd<=0){Attack();MobileInput.Attack=false;}
    }
    void Attack(){
        cd=.35f;
        Vector3 center=transform.position+transform.forward*1.5f+Vector3.up;
        foreach(var c in Physics.OverlapSphere(center,2.1f)){var e=c.GetComponent<EnemyAI>();if(e){e.Hit(1);GameScore.Add(100);}}
    }
}
