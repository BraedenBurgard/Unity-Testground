using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LevelController : MonoBehaviour
{
    public GameObject N_start1;
        private GameObject[] N_start_options = new GameObject[1];
    public GameObject N_normal1, N_normal2, N_normal3, N_normal4, N_normal5, N_normal6, N_normal7;
        private GameObject[] N_normal_options = new GameObject[7];
    public GameObject NE_normal1, NE_normal2, NE_normal3, NE_normal4, NE_normal5, NE_normal6, NE_normal7;
        private GameObject[] NE_normal_options = new GameObject[7];
    public GameObject NS_normal1, NS_normal2, NS_normal3, NS_normal4, NS_normal5, NS_normal6, NS_normal7;
        private GameObject[] NS_normal_options = new GameObject[7];
    public GameObject NES_normal1, NES_normal2, NES_normal3, NES_normal4, NES_normal5, NES_normal6, NES_normal7;
        private GameObject[] NES_normal_options = new GameObject[7];
    public GameObject NESW_normal1, NESW_normal2, NESW_normal3, NESW_normal4, NESW_normal5, NESW_normal6, NESW_normal7;
        private GameObject[] NESW_normal_options = new GameObject[7];
    public GameObject N_treasure1, N_treasure2, N_treasure3, N_treasure4, N_treasure5, N_treasure6, N_treasure7;
        private GameObject[] N_treasure_options = new GameObject[7];
    public GameObject N_boss1, N_boss2, N_boss3, N_boss4, N_boss5, N_boss6, N_boss7;
        private GameObject[] N_boss_options = new GameObject[7];
    public GameObject N_exit1, N_exit2, N_exit3, N_exit4, N_exit5, N_exit6, N_exit7;
        private GameObject[] N_exit_options = new GameObject[7];

    [System.NonSerialized]
    public string[,] levelMap = new string[200,200];
    [System.NonSerialized]
    public GameObject[,] allRooms = new GameObject[200,200];
    [System.NonSerialized]
    public int startx, starty;

    private int numRecursions = 0;
    void Start()
    {
        if(N_start1 != null) {N_start_options[0] = N_start1;}

        if(N_normal1 != null) {N_normal_options[0] = N_normal1;}
        if(N_normal2 != null) {N_normal_options[1] = N_normal2;}
        if(N_normal3 != null) {N_normal_options[2] = N_normal3;}
        if(N_normal4 != null) {N_normal_options[3] = N_normal4;}
        if(N_normal5 != null) {N_normal_options[4] = N_normal5;}
        if(N_normal6 != null) {N_normal_options[5] = N_normal6;}
        if(N_normal7 != null) {N_normal_options[6] = N_normal7;}

        if(NE_normal1 != null) {NE_normal_options[0] = NE_normal1;}
        if(NE_normal2 != null) {NE_normal_options[1] = NE_normal2;}
        if(NE_normal3 != null) {NE_normal_options[2] = NE_normal3;}
        if(NE_normal4 != null) {NE_normal_options[3] = NE_normal4;}
        if(NE_normal5 != null) {NE_normal_options[4] = NE_normal5;}
        if(NE_normal6 != null) {NE_normal_options[5] = NE_normal6;}
        if(NE_normal7 != null) {NE_normal_options[6] = NE_normal7;}

        if(NS_normal1 != null) {NS_normal_options[0] = NS_normal1;}
        if(NS_normal2 != null) {NS_normal_options[1] = NS_normal2;}
        if(NS_normal3 != null) {NS_normal_options[2] = NS_normal3;}
        if(NS_normal4 != null) {NS_normal_options[3] = NS_normal4;}
        if(NS_normal5 != null) {NS_normal_options[4] = NS_normal5;}
        if(NS_normal6 != null) {NS_normal_options[5] = NS_normal6;}
        if(NS_normal7 != null) {NS_normal_options[6] = NS_normal7;}

        if(NES_normal1 != null) {NES_normal_options[0] = NES_normal1;}
        if(NES_normal2 != null) {NES_normal_options[1] = NES_normal2;}
        if(NES_normal3 != null) {NES_normal_options[2] = NES_normal3;}
        if(NES_normal4 != null) {NES_normal_options[3] = NES_normal4;}
        if(NES_normal5 != null) {NES_normal_options[4] = NES_normal5;}
        if(NES_normal6 != null) {NES_normal_options[5] = NES_normal6;}
        if(NES_normal7 != null) {NES_normal_options[6] = NES_normal7;}

        if(NESW_normal1 != null) {NESW_normal_options[0] = NESW_normal1;}
        if(NESW_normal2 != null) {NESW_normal_options[1] = NESW_normal2;}
        if(NESW_normal3 != null) {NESW_normal_options[2] = NESW_normal3;}
        if(NESW_normal4 != null) {NESW_normal_options[3] = NESW_normal4;}
        if(NESW_normal5 != null) {NESW_normal_options[4] = NESW_normal5;}
        if(NESW_normal6 != null) {NESW_normal_options[5] = NESW_normal6;}
        if(NESW_normal7 != null) {NESW_normal_options[6] = NESW_normal7;}

        if(N_treasure1 != null) {N_treasure_options[0] = N_treasure1;}
        if(N_treasure2 != null) {N_treasure_options[1] = N_treasure2;}
        if(N_treasure3 != null) {N_treasure_options[2] = N_treasure3;}
        if(N_treasure4 != null) {N_treasure_options[3] = N_treasure4;}
        if(N_treasure5 != null) {N_treasure_options[4] = N_treasure5;}
        if(N_treasure6 != null) {N_treasure_options[5] = N_treasure6;}
        if(N_treasure7 != null) {N_treasure_options[6] = N_treasure7;}

        if(N_boss1 != null) {N_boss_options[0] = N_boss1;}
        if(N_boss2 != null) {N_boss_options[1] = N_boss2;}
        if(N_boss3 != null) {N_boss_options[2] = N_boss3;}
        if(N_boss4 != null) {N_boss_options[3] = N_boss4;}
        if(N_boss5 != null) {N_boss_options[4] = N_boss5;}
        if(N_boss6 != null) {N_boss_options[5] = N_boss6;}
        if(N_boss7 != null) {N_boss_options[6] = N_boss7;}

        if(N_exit1 != null) {N_exit_options[0] = N_exit1;}
        if(N_exit2 != null) {N_exit_options[1] = N_exit2;}
        if(N_exit3 != null) {N_exit_options[2] = N_exit3;}
        if(N_exit4 != null) {N_exit_options[3] = N_exit4;}
        if(N_exit5 != null) {N_exit_options[4] = N_exit5;}
        if(N_exit6 != null) {N_exit_options[5] = N_exit6;}
        if(N_exit7 != null) {N_exit_options[6] = N_exit7;}
    }

    //generates the level. Called from LevelLayout script in same GameObject
    public void GenerateLevel(string[,] givenMap, int totalRooms)
    {
        //copy given map to local level map
        for(int i = 0; i < levelMap.GetLength(0); i++)
        {
            for(int j = 0; j < levelMap.GetLength(1); j++)
            {
                levelMap[i,j] = givenMap[i,j];
            }
        }


        //first, fill the indexes with rooms. Set their rotation, but not location
        bool isNull = false;
        for(int i = 0; i < levelMap.GetLength(0); i++)
        {
            for(int j = 0; j < levelMap.GetLength(1); j++)
            {
                switch(levelMap[i,j])
                {
                    case "empty":
                        allRooms[i,j] = null;
                        isNull = true;
                        break;

                    //start is a special case, and starts active
                    case "start":
                        startx = i; starty = j;
                        allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(N_start_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                        break;

                    case "normal":
                        switch(CountNeighbors(i,j))
                        {
                            case 1:
                                
                                allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(N_normal_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                allRooms[i,j].SetActive(false);
                                break;
                            case 2:
                                if(IsNS(i,j))
                                {
                                    allRooms[i,j] = (GameObject)Instantiate(
                                                Choose(NS_normal_options), 
                                                new Vector3(0f, 0f, 0f),
                                                Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                    allRooms[i,j].SetActive(false);
                                }
                                else
                                {
                                    allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(NE_normal_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                allRooms[i,j].SetActive(false);
                                }
                                break;
                            case 3:
                                allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(NES_normal_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                allRooms[i,j].SetActive(false);
                                break;
                            case 4:
                                allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(NESW_normal_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                allRooms[i,j].SetActive(false);
                                break;
                        }
                        break;
                    case "treasure":
                        switch(CountNeighbors(i,j))
                        {
                            case 1:
                                allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(N_treasure_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                allRooms[i,j].SetActive(false);
                                break;
                        }
                        break;
                    case "boss":
                        switch(CountNeighbors(i,j))
                        {
                            case 1:
                                allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(N_boss_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                allRooms[i,j].SetActive(false);
                                break;
                        }
                        break;
                    case "exit":
                        switch(CountNeighbors(i,j))
                        {
                            case 1:
                                allRooms[i,j] = (GameObject)Instantiate(
                                            Choose(N_exit_options), 
                                            new Vector3(0f, 0f, 0f),
                                            Quaternion.Euler(new Vector3(0f, DetermineRotation(i,j), 0f)));
                                allRooms[i,j].SetActive(false);
                                break;
                        }
                        break;
                }
                if(!isNull)
                {
                    allRooms[i,j].GetComponent<RoomAttributes>().x = i;
                    allRooms[i,j].GetComponent<RoomAttributes>().y = j;
                    allRooms[i,j].GetComponent<RoomAttributes>().levelName = gameObject.name;
                }
                else{isNull = false;}
            }
        }

        //finally, call a recursive method to iterate through the level and set the position of each room
        int nextx, nexty;
        if(allRooms[startx+1,starty] != null) {nextx = startx+1; nexty = starty;}
        else if(allRooms[startx-1,starty] != null) {nextx = startx-1; nexty = starty;}
        else if(allRooms[startx,starty+1] != null) {nextx = startx; nexty = starty+1;}
        else {nextx = startx; nexty = starty-1;}
        Traverse_and_SetPosition(startx,starty,nextx,nexty, totalRooms);

       //and move the character to spawn
        GameObject.Find("Character").GetComponent<FirstPersonController>().MoveTo(allRooms[startx,starty].transform.Find("SpawnPoint").gameObject.transform.position);
    }

    //Given an index with two neighbors, determine if it is a NS room
    bool IsNS(int x, int y)
    {
        if(IsRoom(x-1,y) && IsRoom(x+1,y)) {return true;}
        if(IsRoom(x,y-1) && IsRoom(x,y+1)) {return true;}
        return false;
    }
    //determine what rotation a room should have
    float DetermineRotation(int x, int y)
    {
        int rand;
        int numConnected = CountNeighbors(x, y);
        switch(numConnected)
        {
            case 1:
                if(IsRoom(x,y+1)){return 0f;}
                if(IsRoom(x+1,y)){return 90f;}
                if(IsRoom(x,y-1)){return 180f;}
                return 270f;
            case 2:
                if(IsRoom(x,y+1) && IsRoom(x+1,y)) {return 0f;}
                if(IsRoom(x+1,y) && IsRoom(x,y-1)) {return 90f;}
                if(IsRoom(x,y-1) && IsRoom(x-1,y)) {return 180f;}
                if(IsRoom(x,y+1) && IsRoom(x-1,y)) {return 270f;}
                rand = Random.Range(1,2);
                if(rand == 1 && IsRoom(x,y+1) && IsRoom(x,y-1)) {return 0f;}
                if(rand == 2 && IsRoom(x,y+1) && IsRoom(x,y-1)) {return 180f;}
                if(rand == 1) {return 90f;}
                return 270f;
            case 3:
                if(!IsRoom(x-1,y)){return 0f;}
                if(!IsRoom(x,y+1)){return 90f;}
                if(!IsRoom(x+1,y)){return 180f;}
                return 270f;
            case 4:
                rand = Random.Range(1,4);
                switch(rand)
                {
                    case 1: return 0f;
                    case 2: return 90f;
                    case 3: return 180f;
                    case 4: return 270f;
                }
                break;
        }

        Debug.LogError("DetermineRotation failed");
        return 0f;
    }
    //helper to DetermineRotation
    bool IsRoom(int x, int y)
    {
        if(x > 200 || x < 0 || y > 200 || y < 0) {return false;}
        if(levelMap[x,y] == "empty") {return false;}
        return true;
    }
    int CountNeighbors(int x, int y)
    {
        int numConnected = 0;
        if(IsRoom(x+1,y)) {numConnected++;}
        if(IsRoom(x-1,y)) {numConnected++;}
        if(IsRoom(x,y+1)) {numConnected++;}
        if(IsRoom(x,y-1)) {numConnected++;}
        return numConnected;
    }

    //chooses a random room from given array
    GameObject Choose(GameObject[] options)
    {
        int valid = 0;
        for(int i = 0; i < options.GetLength(0); i++)
        {
            if(options[i] != null) {valid++;}
        }
        if(valid == 0) {Debug.LogError("No room options available"); return null;}

        int choice = Random.Range(0, valid-1);
        int index = 0;
        while(true)
        {
            //first if statement is the exit condition
            if(choice == 0 && options[index] != null) {return options[index];}
            else if(choice == 0) {index++;}
            else if(options[index] != null) {choice--; index++;}
            else {index++;}
        }
    }

    //recursively traverses all of the rooms, starting from start. Moves them to their proper locations
    void Traverse_and_SetPosition(int oldx, int oldy, int newx, int newy, int failsafe)
    {
        numRecursions++;

        string direction = "";
        if(newx > oldx) {direction = "E";}
        if(newx < oldx) {direction = "W";}
        if(newy > oldy) {direction = "N";}
        if(newy < oldy) {direction = "S";}
        
        GameObject oldReference = null;
        switch(allRooms[oldx,oldy].transform.rotation.eulerAngles.y)
        {
            case 0f:
                switch(direction)
                {
                    case "N": oldReference = allRooms[oldx,oldy].transform.Find("Nreference").gameObject; break;
                    case "E": oldReference = allRooms[oldx,oldy].transform.Find("Ereference").gameObject; break;
                    case "S": oldReference = allRooms[oldx,oldy].transform.Find("Sreference").gameObject; break;
                    case "W": oldReference = allRooms[oldx,oldy].transform.Find("Wreference").gameObject; break;
                }
                break;
            case 90f:
                switch(direction)
                {
                    case "N": oldReference = allRooms[oldx,oldy].transform.Find("Wreference").gameObject; break;
                    case "E": oldReference = allRooms[oldx,oldy].transform.Find("Nreference").gameObject; break;
                    case "S": oldReference = allRooms[oldx,oldy].transform.Find("Ereference").gameObject; break;
                    case "W": oldReference = allRooms[oldx,oldy].transform.Find("Sreference").gameObject; break;
                }
                break;
            case 180f:
                switch(direction)
                {
                    case "N": oldReference = allRooms[oldx,oldy].transform.Find("Sreference").gameObject; break;
                    case "E": oldReference = allRooms[oldx,oldy].transform.Find("Wreference").gameObject; break;
                    case "S": oldReference = allRooms[oldx,oldy].transform.Find("Nreference").gameObject; break;
                    case "W": oldReference = allRooms[oldx,oldy].transform.Find("Ereference").gameObject; break;
                }
                break;
            case 270f:
                switch(direction)
                {
                    case "N": oldReference = allRooms[oldx,oldy].transform.Find("Ereference").gameObject; break;
                    case "E": oldReference = allRooms[oldx,oldy].transform.Find("Sreference").gameObject; break;
                    case "S": oldReference = allRooms[oldx,oldy].transform.Find("Wreference").gameObject; break;
                    case "W": oldReference = allRooms[oldx,oldy].transform.Find("Nreference").gameObject; break;
                }
                break;    
        }

        GameObject newReference = null;
        switch(allRooms[newx,newy].transform.rotation.eulerAngles.y)
        {
            case 0f:
                switch(direction)
                {
                    case "N": newReference = allRooms[newx,newy].transform.Find("Sreference").gameObject; break;
                    case "E": newReference = allRooms[newx,newy].transform.Find("Wreference").gameObject; break;
                    case "S": newReference = allRooms[newx,newy].transform.Find("Nreference").gameObject; break;
                    case "W": newReference = allRooms[newx,newy].transform.Find("Ereference").gameObject; break;
                }
                break;    
            case 90f:
                switch(direction)
                {
                    case "N": newReference = allRooms[newx,newy].transform.Find("Ereference").gameObject; break;
                    case "E": newReference = allRooms[newx,newy].transform.Find("Sreference").gameObject; break;
                    case "S": newReference = allRooms[newx,newy].transform.Find("Wreference").gameObject; break;
                    case "W": newReference = allRooms[newx,newy].transform.Find("Nreference").gameObject; break;
                }
                break;    
            case 180f:
                switch(direction)
                {
                    case "N": newReference = allRooms[newx,newy].transform.Find("Nreference").gameObject; break;
                    case "E": newReference = allRooms[newx,newy].transform.Find("Ereference").gameObject; break;
                    case "S": newReference = allRooms[newx,newy].transform.Find("Sreference").gameObject; break;
                    case "W": newReference = allRooms[newx,newy].transform.Find("Wreference").gameObject; break;
                }
                break;    
            case 270f:
                switch(direction)
                {
                    case "N": newReference = allRooms[newx,newy].transform.Find("Wreference").gameObject; break;
                    case "E": newReference = allRooms[newx,newy].transform.Find("Nreference").gameObject; break;
                    case "S": newReference = allRooms[newx,newy].transform.Find("Ereference").gameObject; break;
                    case "W": newReference = allRooms[newx,newy].transform.Find("Sreference").gameObject; break;
                }
                break;    
        }

        //match reference points
        Vector3 newLocation = oldReference.transform.position - newReference.transform.position;
        allRooms[newx,newy].transform.position += newLocation;


        //this exit condition avoids endless loops, but should never be reached
        failsafe--;
        if(failsafe == 0) {Debug.Log("Traverse failsafe reached"); return;}

        //recursive call on other connected rooms
        //exit condition is when no if statements pass
        if(IsRoom(newx+1, newy) && newx+1 != oldx)
        {Traverse_and_SetPosition(newx,newy, newx+1,newy, failsafe);}
        if(IsRoom(newx-1, newy) && newx-1 != oldx)
        {Traverse_and_SetPosition(newx,newy, newx-1,newy, failsafe);}
        if(IsRoom(newx, newy+1) && newy+1 != oldy)
        {Traverse_and_SetPosition(newx,newy, newx,newy+1, failsafe);}
        if(IsRoom(newx, newy-1) && newy-1 != oldy)
        {Traverse_and_SetPosition(newx,newy, newx,newy-1, failsafe);}
        
    }

    public void SetNeighboursActive(int x, int y)
    {
        setActive(x+1,y,true);
        setActive(x-1,y,true);
        setActive(x,y+1,true);
        setActive(x,y-1,true);

        setActive(x+2,y,false);
        setActive(x+1,y+1,false);
        setActive(x,y+2,false);
        setActive(x-1,y+1,false);
        setActive(x-2,y,false);
        setActive(x-1,y-1,false);
        setActive(x,y-2,false);
        setActive(x+1,y-1,false);
    }
    void setActive(int x, int y, bool aBool)
    {
        if(allRooms[x,y] != null) {allRooms[x,y].SetActive(aBool);}
    }
}
