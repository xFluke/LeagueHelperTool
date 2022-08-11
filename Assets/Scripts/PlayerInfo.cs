using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo
{
    [SerializeField] Image championImage;

    public string Name { get; private set; }
    public string Champion { get; private set; }
    public int Kills { get; private set; }
    public int Deaths { get; private set; }
    public int Assists { get; private set; }

    public PlayerInfo(string name, string champion) {
        this.Name = name;
        this.Champion = champion;
    }
}
