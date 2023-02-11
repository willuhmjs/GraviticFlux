using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    MoveLeft,
    MoveRight,
    Jump,
    FlipGravity,
    Reset
}

[System.Serializable]
public class SettingsData
{
    public int latestLevel {get; set;} = 0;

    internal Dictionary<PlayerAction, KeyCode> keybinds {get; set;} = new Dictionary<PlayerAction, KeyCode>() {
        {PlayerAction.MoveLeft, KeyCode.A},
        {PlayerAction.MoveRight, KeyCode.D},
        {PlayerAction.Jump, KeyCode.Space},
        {PlayerAction.FlipGravity, KeyCode.Space},
        {PlayerAction.Reset, KeyCode.R}
    };
}
