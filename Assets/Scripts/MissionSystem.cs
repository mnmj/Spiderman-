using UnityEngine;
public class MissionSystem:MonoBehaviour
{
    int defeated;
    void Update(){
        defeated=FindObjectsOfType<EnemyAI>().Length;
        if(GameBootstrap.MissionText)GameBootstrap.MissionText.text=defeated>0?"MISSION: Defeat the attackers ("+defeated+")":"MISSION COMPLETE!";
    }
}
