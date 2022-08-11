using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Camille.RiotGames;
using Camille.Enums;
using UnityEngine.Networking;

public class RiotAPIHandler : MonoBehaviour
{
    [SerializeField] string API_KEY;
    //private RiotApi riotAPI;
    private RiotGamesApi riotAPI;  

    private Dictionary<int, string> ChampionIDDictionary;

    [SerializeField] GameObject playerInfoPanelPrefab;
    [SerializeField] Transform parent;

    private void Start() {

        riotAPI = RiotGamesApi.NewInstance(API_KEY);

        var match = riotAPI.MatchV5().GetMatchAsync(RegionalRoute.AMERICAS, "NA1_4401423195").Result;
        foreach (var participant in match.Info.Participants) {
            PlayerInfo playerInfo = new PlayerInfo(participant.SummonerName, participant.ChampionName);
            GameObject playerInfoPanel = Instantiate(playerInfoPanelPrefab, parent);
            playerInfoPanel.GetComponent<PlayerInfoPanel>().SetChampionImage(participant.ChampionName);
        }
    }

}
