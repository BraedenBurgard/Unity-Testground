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

    public int generationAttempts;

    [Tooltip("Just tempType a name, path and .txt added automatically")]
    public string outFile;


    private string[,] levelMap = new string[200,200];
    private int[] startCoord = new int[2];


    void Start()
    {
        bool success = false;
        int tries = 0;
        while(!success && tries < generationAttempts)
        {
            //reset to empty on each failed attempt
            for(int i = 0; i < 200; i++)
             {
                for(int j = 0; j < 200; j++)
                {
                    levelMap[i,j] = "empty";
                }
            }
            if(levelType.ToLower() == "linear") {success = GenerateLinear(true, 100, 100, "+x", totalRooms, numTreasureRooms, false, false);}
            tries++;
        }

        //send the completed map to LevelController
        //print level layout to debug
        if(success) {MapToFile(); GetComponent<LevelController>().GenerateLevel(levelMap, totalRooms);}
        else {Debug.Log("Failed " + generationAttempts + " times");}
    }

    //goes straight, no branching
    bool GenerateLinear(bool isStart, int startX, int startY, string startDirection, int rooms, int treasureRooms, bool bossRoom, bool exitRoom)
    {
        int roomsCopy = rooms;
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

        //add treasure rooms if necessary, jutting out from main path
        if(treasureRooms > 0)
        {
            string tempDirection;
            if(IsRoom(startX+1,startY)) {tempDirection = "+x";}
            else if(IsRoom(startX-1,startY)) {tempDirection = "-x";}
            else if(IsRoom(startX,startY+1)) {tempDirection = "+y";}
            else {tempDirection = "-y";}
            if(!TraverseAndAddExtras(startX, startY, tempDirection, "treasure", treasureRooms, roomsCopy-1)) {return false;}
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
        if(x > 200 || y > 200) {return false;}
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

    void WriteString(string writeThis)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter("Assets/LevelGeneration/" + outFile + ".txt", true);
        writer.WriteLine(writeThis);
        writer.Close();

    }


    //print map to debug log
    void MapToFile()
    {
        string printThis = "";

        for(int i = 0; i < 200; i++)
        {
            for(int j = 0; j < 200; j++)
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
                    case "boss":
                        printThis += "B";
                        break;
                    case "exit":
                        printThis += "E";
                        break;
                    case "treasure":
                        printThis += "T";
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

    bool IsRoom(int x, int y)
    {
        if(x > 200 || x < 0 || y > 200 || y < 0) {return false;}
        if(levelMap[x,y] == "empty") {return false;}
        return true;
    }

    //returns false when out of bound exceptions occur
    bool IsNotRoom(int x, int y)
    {
        if(x > 200 || x < 0 || y > 200 || y < 0) {return false;}
        if(levelMap[x,y] == "empty") {return true;}
        return false;
    }

    //regular method for traversing the level
    void Traverse(int oldx, int oldy, int newx, int newy, int failsafe)
    {
        failsafe--;
        if(failsafe == 0) {Debug.Log("Traverse failsafe reached"); return;}

        string direction = "";
        if(newx > oldx) {direction = "+x";}
        if(newx < oldx) {direction = "-x";}
        if(newy > oldy) {direction = "+y";}
        if(newy < oldy) {direction = "-y";}

        //recursive call on other connected rooms
        //exit condition is when no if statements pass
        if(IsRoom(newx+1, newy) && newx+1 != oldx)
        {Traverse(newx,newy, newx+1,newy, failsafe);}
        if(IsRoom(newx-1, newy) && newx-1 != oldx)
        {Traverse(newx,newy, newx-1,newy, failsafe);}
        if(IsRoom(newx, newy+1) && newy+1 != oldy)
        {Traverse(newx,newy, newx,newy+1, failsafe);}
        if(IsRoom(newx, newy-1) && newy-1 != oldy)
        {Traverse(newx,newy, newx,newy-1, failsafe);}
    }
    
    //adds specified number of treasures at random places along given branch (AFTER given x,y). Only at normal rooms
    private int tempRemainingExtraRooms;
    private string tempType;
    private float tempChance;
    bool TraverseAndAddExtras(int x, int y, string direction, string type, int amount, int roomsInBranch)
    {
        tempRemainingExtraRooms = amount;
        tempType = type;
        tempChance = 4f / roomsInBranch;
        int newx = 0;
        int newy = 0;
        switch(direction)
        {
            case "+x": newx = x+1; newy = y; break;
            case "-x": newx = x-1; newy = y; break;
            case "+y": newx = x; newy = y+1; break;
            case "-y": newx = x; newy = y-1; break;
        }
        int failsafe = amount*10;

        while(tempRemainingExtraRooms > 0 && failsafe >= 0)
        {TraverseAndAddExtrasRecursive(x, y, newx, newy, totalRooms); 
        failsafe--;}

        //true if successful, false if failed to put the right number of treasureRooms
        if(tempRemainingExtraRooms < 0) {Debug.Log("TraverseAndAddTreasures put in too many treasure rooms");}
        if(tempRemainingExtraRooms != 0) {return false;}
        return true;
    }
    void TraverseAndAddExtrasRecursive(int oldx, int oldy, int newx, int newy, int failsafe)
    {

        failsafe--;
        if(failsafe == 0) {Debug.Log("Traverse failsafe reached"); return;}

        string direction = "";
        if(newx > oldx) {direction = "+x";}
        if(newx < oldx) {direction = "-x";}
        if(newy > oldy) {direction = "+y";}
        if(newy < oldy) {direction = "-y";}

        if(tempRemainingExtraRooms <= 0) {return;}
        if(levelMap[newx,newy] == "normal" && Random.Range(0f,1f) < tempChance)
        {
            int rand = Random.Range(1,4);
            
            switch(rand)
            {
                case 1: 
                    if(direction != "+x" && ValidForNext(newx+1, newy)) 
                    {levelMap[newx+1,newy] = tempType; tempRemainingExtraRooms--;}
                    break;
                case 2: 
                    if(direction != "-x" && ValidForNext(newx-1, newy)) 
                    {levelMap[newx-1,newy] = tempType; tempRemainingExtraRooms--;}
                    break;
                case 3: 
                    if(direction != "+y" && ValidForNext(newx, newy+1)) 
                    {levelMap[newx,newy+1] = tempType; tempRemainingExtraRooms--;}
                    break;
                case 4: 
                    if(direction != "-y" && ValidForNext(newx, newy-1)) 
                    {levelMap[newx,newy-1] = tempType; tempRemainingExtraRooms--;}
                    break;
            }
        }

        //recursive call on other connected rooms
        //exit condition is when no if statements pass
        if(IsRoom(newx+1, newy) && newx+1 != oldx)
        {TraverseAndAddExtrasRecursive(newx,newy, newx+1,newy, failsafe);}
        if(IsRoom(newx-1, newy) && newx-1 != oldx)
        {TraverseAndAddExtrasRecursive(newx,newy, newx-1,newy, failsafe);}
        if(IsRoom(newx, newy+1) && newy+1 != oldy)
        {TraverseAndAddExtrasRecursive(newx,newy, newx,newy+1, failsafe);}
        if(IsRoom(newx, newy-1) && newy-1 != oldy)
        {TraverseAndAddExtrasRecursive(newx,newy, newx,newy-1, failsafe);}
    }
}
