using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour
{
    public static Vector2 Move;
    public static Vector2 Look;
    public static bool Jump, Swing, Attack, Sprint;
    static bool lookHeld;
    static Vector2 lastLook;

    public static void Create(Transform parent)
    {
        if(FindObjectOfType<EventSystem>()==null){var es=new GameObject("EventSystem");es.AddComponent<EventSystem>();es.AddComponent<StandaloneInputModule>();}
        var root=new GameObject("TouchControls");root.transform.SetParent(parent,false);
        AddStick(root.transform,new Vector2(0,0),new Vector2(210,210),false);
        AddButton(root.transform,"JUMP",new Vector2(1,0),new Vector2(-120,150),120,()=>Jump=true);
        AddButton(root.transform,"WEB",new Vector2(1,0),new Vector2(-260,120),120,()=>Swing=true);
        AddButton(root.transform,"ATTACK",new Vector2(1,0),new Vector2(-120,300),110,()=>Attack=true);
        AddButton(root.transform,"RUN",new Vector2(0,0),new Vector2(330,150),100,()=>Sprint=true);
    }
    static void AddStick(Transform p,Vector2 a,Vector2 pos,Vector2 unused,bool x){}
    static void AddStick(Transform p,Vector2 anchor,Vector2 size,bool dummy)
    {
        var go=new GameObject("Joystick");go.transform.SetParent(p,false);
        var r=go.AddComponent<RectTransform>();r.anchorMin=r.anchorMax=anchor;r.anchoredPosition=new Vector2(130,130);r.sizeDelta=size;
        var img=go.AddComponent<Image>();img.color=new Color(1,1,1,.12f);
        var trig=go.AddComponent<JoystickArea>();trig.size=size;
    }
    static void AddButton(Transform p,string text,Vector2 anchor,Vector2 pos,float size,UnityEngine.Events.UnityAction act)
    {
        var go=new GameObject(text);go.transform.SetParent(p,false);
        var r=go.AddComponent<RectTransform>();r.anchorMin=r.anchorMax=anchor;r.anchoredPosition=pos;r.sizeDelta=new Vector2(size,size);
        var b=go.AddComponent<Button>();var img=go.AddComponent<Image>();img.color=new Color(.55f,.03f,.08f,.72f);b.targetGraphic=img;b.onClick.AddListener(act);
        var t=new GameObject("Text");t.transform.SetParent(go.transform,false);var tr=t.AddComponent<RectTransform>();tr.anchorMin=Vector2.zero;tr.anchorMax=Vector2.one;tr.offsetMin=tr.offsetMax=Vector2.zero;
        var tx=t.AddComponent<Text>();tx.text=text;tx.font=Resources.GetBuiltinResource<Font>("Arial.ttf");tx.fontSize=20;tx.alignment=TextAnchor.MiddleCenter;tx.color=Color.white;
    }
    void Update()
    {
        if(Input.touchCount==0){Look=Vector2.zero;return;}
        Jump=Swing=Attack=false;
    }
    public class JoystickArea:MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
    {
        public Vector2 size;
        Vector2 center;
        public void Start(){center=GetComponent<RectTransform>().anchoredPosition;}
        public void OnPointerDown(PointerEventData e){OnDrag(e);}
        public void OnPointerUp(PointerEventData e){Move=Vector2.zero;}
        public void OnDrag(PointerEventData e){
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform,e.position,e.pressEventCamera,out var p);
            Move=Vector2.ClampMagnitude(new Vector2(p.x/(size.x*.5f),p.y/(size.y*.5f)),1);
        }
    }
}
