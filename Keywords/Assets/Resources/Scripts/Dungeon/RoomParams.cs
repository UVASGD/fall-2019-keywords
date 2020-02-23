using UnityEngine;
using System;

public enum RoomID {
    Player1Start = -1,
    Player2Start = -2,
    Player3Start = -3,
    Player4Start = -4,
    CentralChamber = -5,
    Team1Start = -6,
    Team2Start = -7
};
[Serializable]
public struct RoomParams {
    public string name;
    public RoomID id;
    public int x;
    public int y;
    public int width;
    public int height;

}