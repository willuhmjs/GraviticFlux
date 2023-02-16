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

public enum Achievements {
    MenuMaverick,
    Gravitic,
    Mastermind
}

[System.Serializable]
public class SettingsData
{
    public int latestLevel {get; set;} = 1;

    internal Dictionary<PlayerAction, KeyCode> controls {get; set;} = new Dictionary<PlayerAction, KeyCode>() {
        {PlayerAction.MoveLeft, KeyCode.A},
        {PlayerAction.MoveRight, KeyCode.D},
        {PlayerAction.Jump, KeyCode.W},
        {PlayerAction.FlipGravity, KeyCode.Space},
        {PlayerAction.Reset, KeyCode.R}
    };

    List<Achievements> achievements {get;} = new List<Achievements>();

    public void AddAchievement(Achievements achievement) {
        if (!achievements.Contains(achievement)) achievements.Add(achievement);
    }
}
