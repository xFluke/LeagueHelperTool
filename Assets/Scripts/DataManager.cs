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

        foreach (var match in matchList) {
            CreateMatchInfoPanel(match);
        }
    }

    private void CreateMatchInfoPanel(string matchID) {
        GameObject matchInfoPanel = Instantiate(matchInfoPanelPrefab, matchInfoParent);
        matchInfoPanel.GetComponent<MatchInfoPanel>().Initialize(matchID);
    }

}
