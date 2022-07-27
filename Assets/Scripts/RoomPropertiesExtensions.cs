using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Linq;
using UnityEngine;

public static class RoomPropertiesExtensions
{
    private const string PlayerIDKeys = "PlayerIDKeys";
    private const string WatcherIDKeys = "WatcherIDKeys";

    private static readonly Hashtable propsToSet = new Hashtable();

    public static List<int> GetPlayerIDList(this Room room)
    {
        string playerIDListStr = (room.CustomProperties[PlayerIDKeys] is string playerIDs) ? playerIDs : "";

        List<int> playerIDList;

        if (playerIDListStr != "")
        {
            playerIDList = playerIDListStr.Split(',').Select(a => int.Parse(a)).ToList();
        }
        else
        {
            playerIDList = new List<int>();
        }
        return playerIDList;
    }

    public static void AddPlayerID(this Room room, int value)
    {
        List<int> playerIDList = room.GetPlayerIDList();

        playerIDList.Add(value);

        propsToSet[PlayerIDKeys] = string.Join(",", playerIDList);
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public static void DeletePlayerID(this Room room, int value)
    {
        List<int> playerIDList = room.GetPlayerIDList();

        playerIDList.Remove(value);

        propsToSet[PlayerIDKeys] = string.Join(",", playerIDList);
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public static List<int> GetWatcherIDList(this Room room)
    {
        string watcherIDListStr = (room.CustomProperties[WatcherIDKeys] is string watcherIDs) ? watcherIDs : "";

        List<int> watcherIDList;

        if (watcherIDListStr != "")
        {
            watcherIDList = watcherIDListStr.Split(',').Select(a => int.Parse(a)).ToList();
        }
        else
        {
            watcherIDList = new List<int>();
        }
        return watcherIDList;
    }

    public static void AddWatcherID(this Room room, int value)
    {
        List<int> watcherIDList = room.GetPlayerIDList();

        watcherIDList.Add(value);

        propsToSet[WatcherIDKeys] = string.Join(",", watcherIDList);
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public static void DeleteWatcherID(this Room room, int value)
    {
        List<int> watcherIDList = room.GetPlayerIDList();

        watcherIDList.Remove(value);

        propsToSet[WatcherIDKeys] = string.Join(",", watcherIDList);
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}
