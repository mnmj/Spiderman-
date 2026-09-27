using UnityEngine;
public class EnemyAI:MonoBehaviour
{
    int hp=2; Transform player; float cd;
    void Start(){player=GameBootstrap.Player;var body=GameObject.CreatePrimitive(PrimitiveType.Capsule);body.transform.SetParent(transform);body.transform.localPosition=Vector3.up;body.transform.localScale=new Vector3(.7f,1.1f,.7f);body.GetComponent<Collider>().enabled=false;body.GetComponent<Renderer>().material=GameBootstrap.Mat(new Color(.08f,.08f,.1f));}
    void Update(){if(!player)return;Vector3 d=player.position-transform.position;d.y=0;if(d.magnitude<25&&d.magnitude>2){transform.position+=d.normalized*2.2f*Time.deltaTime;transform.forward=d.normalized;}if(cd>0)cd-=Time.deltaTime;}
    public void Hit(int n){hp-=n; if(hp<=0){GameScore.Add(250);Destroy(gameObject);}}
}
