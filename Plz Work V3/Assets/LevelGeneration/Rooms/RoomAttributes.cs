using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAttributes : MonoBehaviour
{
    //N, NE, NS, NES, or NESW? (Used for debugging)
    [Tooltip("N, NE, NS, NES, or NESW (north, east, south, west)")]
    public string exitOrientation;
    //what kind of room is this? (Used for debugging)
    [Tooltip("What type of room is this? Use all lowercase")]
    public string type;

    [System.NonSerialized]
    public int x, y;
}
