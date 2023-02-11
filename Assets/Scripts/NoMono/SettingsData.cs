
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public int latestLevel {get; set;} = 0;
    Dictionary<KeyCode, PlayerAction> keybinds {get; set;} = new Dictionary<KeyCode, PlayerAction>() {
        {KeyCode.W, PlayerAction.Jump},
        {KeyCode.A, PlayerAction.MoveLeft},
        {KeyCode.D, PlayerAction.MoveRight},
        {KeyCode.Space, PlayerAction.FlipGravity},
        {KeyCode.R, PlayerAction.Reset}
    };
}