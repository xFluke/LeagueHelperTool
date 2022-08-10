using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiotSharp;
public class RiotAPIHandler : MonoBehaviour
{
    [SerializeField] string API_KEY;
    private RiotApi riotAPI;
    private void Start() {
        riotAPI = RiotApi.GetDevelopmentInstance(API_KEY);
    }
}
