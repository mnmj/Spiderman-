using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameBootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Boot()
    {
        if (FindObjectOfType<GameBootstrap>() != null) return;
        var go = new GameObject("GAME");
        go.AddComponent<GameBootstrap>();
    }

    public static Transform Player;
    public static Camera MainCam;
    public static Text MissionText;
    public static Text ScoreText;

    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        QualitySettings.vSyncCount = 0;
        BuildWorld();
        BuildUI();
    }

    void BuildWorld()
    {
        RenderSettings.ambientLight = new Color(.48f,.52f,.60f);
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(.08f,.11f,.17f);
        RenderSettings.fogDensity = .006f;

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "CityGround";
        ground.transform.localScale = Vector3.one * 20f;
        ground.GetComponent<Renderer>().material = Mat(new Color(.045f,.055f,.07f));

        var city = new GameObject("OpenWorldCity");
        city.AddComponent<CityGenerator>().Generate();

        var p = new GameObject("SpiderHero");
        p.tag = "Player";
        p.transform.position = new Vector3(0,2,0);
        var cc=p.AddComponent<CharacterController>();
        cc.height=1.9f; cc.radius=.35f; cc.center=Vector3.up*.95f;
        p.AddComponent<PlayerMotor>();
        p.AddComponent<WebSwing>();
        p.AddComponent<PlayerCombat>();
        var body=GameObject.CreatePrimitive(PrimitiveType.Capsule);
        body.name="HeroBody"; body.transform.SetParent(p.transform);
        body.transform.localPosition=Vector3.up*.95f;
        body.transform.localScale=new Vector3(.45f,.95f,.45f);
        body.GetComponent<Collider>().enabled=false;
        body.GetComponent<Renderer>().material=Mat(new Color(.72f,.03f,.08f));
        Player=p.transform;

        var cam=new GameObject("MainCamera");
        MainCam=cam.AddComponent<Camera>();
        MainCam.fieldOfView=72;
        MainCam.tag="MainCamera";
        var rig=cam.AddComponent<CameraRig>();
        rig.target=Player;

        for(int i=0;i<12;i++)
        {
            var e=new GameObject("Enemy_"+i);
            e.transform.position=new Vector3(Random.Range(-120,120),1,Random.Range(-120,120));
            e.AddComponent<EnemyAI>();
        }
        var mission=new GameObject("MissionSystem");
        mission.AddComponent<MissionSystem>();
    }

    void BuildUI()
    {
        var canvas=new GameObject("MobileUI");
        var c=canvas.AddComponent<Canvas>(); c.renderMode=RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>().uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.GetComponent<CanvasScaler>().referenceResolution=new Vector2(1280,720);
        canvas.AddComponent<GraphicRaycaster>();

        MissionText=Label(canvas.transform,"MISSION: Stop the attackers",new Vector2(0,1),new Vector2(30,-25),new Vector2(500,60),22,TextAnchor.MiddleLeft);
        ScoreText=Label(canvas.transform,"SCORE 0",new Vector2(1,1),new Vector2(-30,-25),new Vector2(250,60),22,TextAnchor.MiddleRight);

        MobileInput.Create(canvas.transform);
    }

    static Text Label(Transform parent,string s,Vector2 anchor,Vector2 pos,Vector2 size,int fs,TextAnchor align)
    {
        var go=new GameObject("Label"); go.transform.SetParent(parent,false);
        var r=go.AddComponent<RectTransform>(); r.anchorMin=r.anchorMax=anchor; r.anchoredPosition=pos; r.sizeDelta=size;
        var t=go.AddComponent<Text>(); t.text=s; t.font=Resources.GetBuiltinResource<Font>("Arial.ttf"); t.fontSize=fs; t.alignment=align; t.color=Color.white;
        return t;
    }
    public static Material Mat(Color c){var m=new Material(Shader.Find("Standard"));m.color=c;return m;}
}
