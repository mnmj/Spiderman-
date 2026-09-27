using UnityEngine;
public class CityGenerator:MonoBehaviour
{
    public void Generate(){
        Random.InitState(1234);
        int half=6; float step=34;
        for(int x=-half;x<=half;x++)for(int z=-half;z<=half;z++){
            if(Mathf.Abs(x)%3==0||Mathf.Abs(z)%3==0)continue;
            float h=Random.Range(10,55), w=Random.Range(17,28);
            var b=GameObject.CreatePrimitive(PrimitiveType.Cube);b.name="Building";b.transform.position=new Vector3(x*step,h/2,z*step);b.transform.localScale=new Vector3(w,h,w);
            b.GetComponent<Renderer>().material=GameBootstrap.Mat(new Color(Random.Range(.08f,.18f),Random.Range(.1f,.2f),Random.Range(.14f,.25f)));
            for(int i=0;i<Mathf.Min(4,Mathf.FloorToInt(h/5));i++){var a=new GameObject("WebAnchor");a.transform.position=b.transform.position+new Vector3(Random.Range(-w/2,w/2),h/2+.5f,Random.Range(-w/2,w/2));}
        }
    }
}
