using UnityEngine;
public static class GameScore
{
    static int score;
    public static void Add(int x){score+=x;if(GameBootstrap.ScoreText)GameBootstrap.ScoreText.text="SCORE "+score;}
}
