using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MatchInfoPanel : MonoBehaviour
{
    Camille.RiotGames.MatchV5.Match match;

    [SerializeField] GameObject playerInfoPanelPrefab;
    [SerializeField] Transform blueTeamTransform;
    [SerializeField] Transform redTeamTransform;
    public void Initialize(string matchID) {
        this.match = FindObjectOfType<RiotAPIHandler>().GetMatch(matchID);

        PopulateBlueTeam();
        PopulateRedTeam();
    }

    private void PopulateRedTeam() {
        var players = match.Info.Participants;

        for (int i = 5; i < players.Length; i++) {
            PlayerInfo playerInfo = new PlayerInfo(players[i].SummonerName, players[i].ChampionName, players[i].Kills, players[i].Deaths, players[i].Assists);
            GameObject playerInfoPanel = Instantiate(playerInfoPanelPrefab, redTeamTransform);
            playerInfoPanel.GetComponent<PlayerInfoPanel>().Initialize(playerInfo);
        }
    }

    private void PopulateBlueTeam() {
        var players = match.Info.Participants;

        for (int i = 0; i < 5; i++) {
            PlayerInfo playerInfo = new PlayerInfo(players[i].SummonerName, players[i].ChampionName, players[i].Kills, players[i].Deaths, players[i].Assists);
            GameObject playerInfoPanel = Instantiate(playerInfoPanelPrefab, blueTeamTransform);
            playerInfoPanel.GetComponent<PlayerInfoPanel>().Initialize(playerInfo);
        }
    }
}

