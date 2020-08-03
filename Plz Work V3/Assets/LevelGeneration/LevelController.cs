using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [System.NonSerialized]
    public string[,] levelMap = new string[50,50];
    [System.NonSerialized]
    public GameObject[,] allRooms = new GameObject[50,50];
    [System.NonSerialized]
    public int startx, starty;
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

    }

    //generates the level. Called from LevelLayout script in same GameObject
    public void GenerateLevel(string[,] givenMap)
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
        for(int i = 0; i < levelMap.GetLength(0); i++)
        {
            for(int j = 0; j < levelMap.GetLength(1); j++)
            {
                switch(levelMap[i,j])
                {
                    case "empty":
                        allRooms[i,j] = null;
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
                        
                    
                }
            }
        }

       
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
                if(IsRoom(x,y-1) && IsRoom(x-1,y)) {return 270f;}
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
        if(x > 50 || x < 0 || y > 50 || y < 0) {return false;}
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

        //should never be reached
        return null;
    }
}
