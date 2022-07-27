using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

public static class PlayerPropertiesExtensions
{
    private const string ScoreKey = "Score";
    private const string JoinType = "JoinType";

    private static readonly Hashtable propsToSet = new Hashtable();

    // プレイヤーのスコアを取得する
    public static int GetScore(this Player player)
    {
        return (player.CustomProperties[ScoreKey] is int score) ? score : 0;
    }

    // プレイヤーのスコアを加算する
    public static void AddScore(this Player player, int value)
    {
        propsToSet[ScoreKey] = player.GetScore() + value;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public static void SetJoinType(this Player player, string joinType)
    {
        propsToSet[JoinType] = joinType;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public static string GetJoinType(this Player player)
    {
        return (player.CustomProperties[JoinType] is string joinType) ? joinType : "";
    }
}
