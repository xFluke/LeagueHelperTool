using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] Transform matchInfoParent;
    [SerializeField] GameObject matchInfoPanelPrefab;

    [SerializeField] string gameName;
    [SerializeField] string tagLine;


    private RiotAPIHandler riotAPIHandler;



    private void Start() {
        riotAPIHandler = FindObjectOfType<RiotAPIHandler>();

        var matchList = riotAPIHandler.GetMatchList(gameName, tagLine);

        foreach (var matchID in matchList) {
            CreateMatchInfoPanel(matchID);
            GetAllPlayersInMatch(matchID);
        }
    }

    private void CreateMatchInfoPanel(string matchID) {
        GameObject matchInfoPanel = Instantiate(matchInfoPanelPrefab, matchInfoParent);
        matchInfoPanel.GetComponent<MatchInfoPanel>().Initialize(matchID);
    }

    private void GetAllPlayersInMatch(string matchID) {
        List<PlayerInfo> playerInfos = new List<PlayerInfo>();

        var match = riotAPIHandler.GetMatch(matchID);
        foreach (var player in match.Info.Participants) {
            PlayerInfo playerInfo = new PlayerInfo(player.SummonerName, player.ChampionName, player.Kills, player.Deaths, player.Assists);
            playerInfos.Add(playerInfo);
        }

        FindObjectOfType<GoogleAPIHandler>().AddMatchToSheet(playerInfos);
    }
}
