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

    public string levelName;

    public int x, y;

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            GameObject.Find(levelName).GetComponent<LevelController>().SetNeighboursActive(x,y);
        }
    }
}
