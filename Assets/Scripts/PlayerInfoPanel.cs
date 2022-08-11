using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerInfoPanel : MonoBehaviour
{
    [SerializeField] Image championImage;

    public void SetChampionImage(string championName) {
        StartCoroutine(SetChampionImageCoroutine(championName));
    }

    IEnumerator SetChampionImageCoroutine(string championName) {
        string baseURL = "http://ddragon.leagueoflegends.com/cdn/12.15.1/img/champion/";
        string requestURL = baseURL + championName + ".png";

        UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(requestURL);
        yield return unityWebRequest.SendWebRequest();

        Texture2D texture = DownloadHandlerTexture.GetContent(unityWebRequest);
        var championSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));

        championImage.sprite = championSprite;
    }
}
