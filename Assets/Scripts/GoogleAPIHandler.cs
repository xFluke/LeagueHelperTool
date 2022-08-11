using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Sheets.v4.Data;
using System.Threading;
using Google.Apis.Util.Store;

public class GoogleAPIHandler : MonoBehaviour
{

    [SerializeField] private string spreadsheetID;
    private string sheet = "Sheet1";

    static string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private SheetsService service;

    private int row = 1;

    private void Start() {
        UserCredential credential;

        using (var stream = new FileStream(Application.dataPath + "/credentials.json", FileMode.Open, FileAccess.Read)) {
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
        }

        service = new SheetsService(new BaseClientService.Initializer() {
            HttpClientInitializer = credential,
            ApplicationName = "LeagueHelperTool"
        });
    }
        
    public void AppendRowWithPlayerInfo(PlayerInfo playerInfo) {
        var range = $"{sheet}";
        var valueRange = new ValueRange();

        var objectList = new List<object>() { playerInfo.Name, playerInfo.Champion, playerInfo.Kills, playerInfo.Deaths, playerInfo.Assists };

        valueRange.Values = new List<IList<object>> { objectList };

        var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetID, range);
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

        var appendResponse = appendRequest.Execute();

    }

    public void AddMatchToSheet(List<PlayerInfo> playerInfos) {
        List<ValueRange> valueRangesList = new List<ValueRange>();

        foreach (PlayerInfo playerInfo in playerInfos) {
            var objectList = new List<object>() { playerInfo.Name, playerInfo.Champion, playerInfo.Kills, playerInfo.Deaths, playerInfo.Assists };
            var valueRange = new ValueRange();
            valueRange.Values = new List<IList<object>> { objectList };

            var range = $"{sheet}!A{row}:E{row}";
            valueRange.Range = range;
            valueRangesList.Add(valueRange);

            row++;
        }

        BatchUpdateValuesRequest requestBody = new BatchUpdateValuesRequest();
        requestBody.ValueInputOption = "USER_ENTERED";
        requestBody.Data = valueRangesList;

        SpreadsheetsResource.ValuesResource.BatchUpdateRequest request = service.Spreadsheets.Values.BatchUpdate(requestBody, spreadsheetID);

        BatchUpdateValuesResponse response = request.Execute();
    }
}
