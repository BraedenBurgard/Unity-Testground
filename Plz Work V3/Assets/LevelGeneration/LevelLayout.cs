using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelLayout : MonoBehaviour
{
    [Tooltip("Currently supports: \nLinear")]
    public string levelType;

    [Tooltip("Percent chance (0 to 1) of the path turning")]
    public float turnChance;

    public int totalRooms;

    public int numTreasureRooms;

    public bool hasBossRoom;


    private string[,] levelMap = new string[50,50];
    private int[] startCoord = new int[2];

    void Start()
    {
        bool success = false;
        int tries = 0;

        for(int i = 0; i < 50; i++)
        {
            for(int j = 0; j < 50; j++)
            {
                levelMap[i,j] = "empty";
            }
        }

        while(!success && tries < 100)
        {
            if(levelType.ToLower() == "linear") {success = GenerateLinear(true, 25, 20, "-y", totalRooms, 0, false, false);}
            tries++;
        }


        //print level layout to debug
        if(success) {PrintMapToDebug();}
        else {Debug.Log("Failed 100 times");}
    }

    //goes straight, no branching
    bool GenerateLinear(bool isStart, int startX, int startY, string startDirection, int rooms, int treasureRooms, bool bossRoom, bool exitRoom)
    {

        int[] currentCoord = new int[2]{startX, startY};
        string currentDirection = startDirection;
        bool nextValid;

        //set beginning of this path to spawn if isStart is true
        if(isStart)
        {
            startCoord[0] = startX;
            startCoord[1] = startY;
            levelMap[currentCoord[0],currentCoord[1]] = "start";
        }
        else {levelMap[currentCoord[0],currentCoord[1]] = "normal";}
        rooms--;

        while(rooms > 0)
        {
            switch(currentDirection)
            {
                case "+x":
                    currentCoord[0]++;
                    break;
                case "-x":
                    currentCoord[0]--;
                    break;
                case "+y":
                    currentCoord[1]++;
                    break;
                case "-y":
                    currentCoord[1]--;
                    break;
                default:
                    Debug.Log("Incorrect direction");
                    return false;
            }
            //exit and return false if attempting to fill a filled tile
            if(levelMap[currentCoord[0],currentCoord[1]] != "empty")
            {
                return false;
            }

            levelMap[currentCoord[0],currentCoord[1]] = "normal";
            rooms--;

            currentDirection = DetermineDirection(currentCoord[0],currentCoord[1], currentDirection);
            if(currentDirection == "trapped") {return false;}
        }




        //return true if successfully finished
        Debug.Log("Linear generated successfully");
        return true;
    }

    string DetermineDirection(int x, int y, string currentDirection)
    {
        if(Random.Range(0f, 1f) <= turnChance && DirectionValidForNext(x,y,currentDirection))
        {
                return currentDirection;
        } 
        int leftOrRight = (int)Random.Range(0f,1.999f);
        string right;
        string left;
        switch(currentDirection)
        {
            case "+x":
                left = "+y";
                right = "-y";
                break;
            case "-x":
                left = "-y";
                right = "+y";
                break;
            case "+y":
                left = "-x";
                right = "+x";
                break;
            case "-y":
                left = "+x";
                right = "-x";
                break;
            default:
                Debug.LogError("Invalid direction");
                left = "nothing";
                right = "nothing";
                break;
        }

        if(leftOrRight == 1 && DirectionValidForNext(x,y,left)) {return left;}
        else if(leftOrRight == 0 && DirectionValidForNext(x,y,right)) {return right;}
        else if(DirectionValidForNext(x,y,currentDirection)) {return currentDirection;}
        else {return "trapped";}

    }

    bool DirectionValidForNext(int x, int y, string direction)
    {
        if(x > 50 || y > 50) {return false;}
        switch(direction)
            {
                case "+x":
                    return ValidForNext(x+1, y);
                case "-x":
                    return ValidForNext(x-1, y);
                case "+y":
                    return ValidForNext(x, y+1);
                case "-y":
                    return ValidForNext(x, y-1);
                default:
                    Debug.Log("Incorrect direction");
                    return false;
            }
    }

    //check a position to see if it's valid for a new tile
    bool ValidForNext(int x, int y)
    {
        try{
            if(levelMap[x,y] != "empty") {return false;}

            int neighbors = 0;
            if(levelMap[x+1,y] != "empty") {neighbors++;}
            if(levelMap[x-1,y] != "empty") {neighbors++;}
            if(levelMap[x,y+1] != "empty") {neighbors++;}
            if(levelMap[x,y-1] != "empty") {neighbors++;}

            if(neighbors > 1) {return false;}
            return true;
        }
        catch{
        return false;
        }
    }

    static void WriteString(string writeThis)
    {
        string path = "Assets/LevelGeneration/LevelMap0.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(writeThis);
        writer.Close();

    }


    //print map to debug log
    void PrintMapToDebug()
    {
        string printThis = "";

        for(int i = 0; i < 50; i++)
        {
            for(int j = 0; j < 50; j++)
            {
                switch(levelMap[i,j])
                {
                    case "empty":
                        printThis += " ";
                        break;
                    case "normal":
                        printThis += "O";
                        break;
                    case "start":
                        printThis += "S";
                        break;
                    default:
                        printThis += "?";
                        break;
                }
            }
            printThis += "\n";
        }
        WriteString(printThis);
    }
    
}
