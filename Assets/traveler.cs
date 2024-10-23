using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class traveler : MonoBehaviour
{
    int x;
    int y;
    bool cottageLock=true;
    bool shearsfound = false;
    bool keyfound = false;
    bool inshed = false;
    bool padlock = true;
    bool nailsFound = false;
    bool lanternfound = false;
    bool boatrope=true;
    bool padlockKey=false;
    bool pickaxefound = false;
    public string input;
    int maxcapacity=2;
    List<(string,(int,int),bool)> itemlocations;
    List<string> inventory;
    List<string> tools;
    List<(int, int)> locations;
    bool safelock = true;
    bool safekeyfound = false;
    int[] codes;
    int code;
    public GameObject text;
    public GameObject outputfield;
    // Start is called before the first frame update
    void Start()
    {
        tools = new List<string>() {"ax","hammer","fishing rod"};
        codes = new int[5] { 2051, 9738, 1852, 1259, 8064};
        code = codes[Random.Range(0, 5)];
        inventory = new List<string>();

        x = 10;
        y = 10;
        inventory.Add("fishing rod");
        inventory.Add("hammer");
        inventory.Add("ax");

        locations = new List<(int,int)>()
        {
            (10,21),
            (10,20),
            (10,19),
            (10,18),
            (10,17),
            (10,16),
            (10,14),
            (10,13),
            (10,12),
            (10,11),
            (10,10),
            (10,9),
            (10,8),
            (10,7),
            (10,6),
            (10,5),
            (10,4),
            (10,3),
            (10,2),
            (10,0),
            (10,-1),
            (9,6),
            (8,6),
            (7,6),
            (6,6),
            (7,5),
            (8,5),
            (9,5),
            (6,7),
            (7,7),
            (8,6),
            (8,7),
            (6,8),
            (7,8),
            (8,8),
            (5,6),
            (4,6),
            (4, 5),
            (4,4),
            (4, 3),
            (12,12),
            (11,6),
            (8,21),
            (9,21),
            (11,21),
            (12,21),
            (13,21),
            (12,6),
            (13,6),
            (13,5),
            (13,4),
            (13,3),
            (14,3),
            (14,1),
            (15,1),
            (16,1),
            (17,1),
            (18,1),
            (14,6),
            (15,6),
            (16,6)
        };

        Result("Welcome, traveler. To navigate this mysterious land, enter the direction in which you wish to go (example: n=north)");

        Result("You find yourself on a long road in a rural flat land with trees on either side.");

        Move();
    }

    bool Check(string dir, int X, int Y)
    {

        for (int i = 0; i < locations.Count; i++)
        {
            if (dir == "n")
            {
                if ((X,Y)==(9,6)|| (X, Y) == (7, 6))
                {
                    return false;
                }
                if ((X, Y-1) == locations[i])
                {
                    return true;
                }
            }
            else if (dir == "s")
            {
                if ((X, Y) == (9, 5) || (X, Y) == (7, 5))
                {
                    return false;
                }
                if ((X, Y + 1) == locations[i])
                {
                    return true;
                }
            }
            else if (dir == "w")
            {
                inshed = false;

                //if (inventory.Count > 0)
                //{

                    //Debug.Log(inventory[0]);
                //}
                if ((X, Y) == (5, 6) && inventory.Contains("cottage key"))
                {
                    cottageLock = false;
                }
                if ((X,Y)==(5,6)&&cottageLock)
                {

                    Result("It's locked");
                    return false;
                }
                if ((X - 1, Y) == locations[i])
                {
                    return true;
                }
            }
            else if (dir == "e")
            {
                if ((X + 1, Y) == locations[i])
                {
                    return true;
                }
                if ((X,Y)==(9,5))
                {
                    Result("You need to type 'enter shed.'");
                    return false;
                }
            }
        }
        return false;
    }
    bool Loc(int X, int Y)
    {
        if (X == x && Y == y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Result(string result)
    {
        Debug.Log(result);
        GameObject newtext= Instantiate(text);
        newtext.GetComponent<Text>().text = result;
        newtext.transform.SetParent(outputfield.transform);


        if (GameObject.FindGameObjectsWithTag("output").Length > 6)
        {
            while (GameObject.FindGameObjectsWithTag("output").Length > 6)
            {

                Destroy(outputfield.transform.GetChild(0).gameObject);
            } 
        }
    }

    public void Input(string input)
    {
        if (new List<string>() { "n", "s", "w", "e" }.Contains(input))
        {
            if (input == "n" && Check("n", x, y))
            {
                y--;

                Move();
            }
            else if (input == "s" && Check("s", x, y))
            {
                y++;

                Move();
            }
            else if (input == "w" && Check("w", x, y))
            {
                x--;

                Move();
            }
            else if (input == "e" && Check("e", x, y))
            {
                x++;

                Move();
            }
            else
            {
                if((x, y) != (5, 6))
                {
                    Result("You can't go that way.");
                }
            }
        }
        else
        {
            if ( input == "look under rock"&&!shearsfound)
            {

                if ((x, y) == (8, 8))
                {
                    shearsfound = true;
                    inventory.Add("rusty shears");
                    Result("You found a pair of rusty shears.");
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input == "cut vines")
            {
                if ((x, y) == (7, 5) && inventory.Contains("rusty shears")&&!keyfound)
                {
                    keyfound = true;

                    inventory.Remove("rusty shears");
                    inventory.Add("cottage key");
                    Result("You found a secret compartment behind the vines!");
                    Result("You found an old rusty key!");
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input == "open safe")
            {
                if ((x, y) == (4, 4) && inventory.Contains("safe key") && safelock)
                {
                    safelock = false;
                    Result("You found a numerical code: "+code+".");
                    inventory.Remove("safe key");
                }
                else
                {
                    Result("The safe is locked");
                }
            }
            if (input=="look under carpet")
            {
                if ((x,y)==(4,6))
                {

                    Result("You found a note: 'chandelier'");
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if ((x, y) == (18, 1))
            {
                if (input=="open chest"&&!lanternfound)
                {
                    Result("You got a lantern.");
                    inventory.Add("lantern");
                    lanternfound = true;
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input=="check chandelier")
            {
                if ((x,y)==(4,5)&&!safekeyfound)
                {
                    safekeyfound = true;
                    Result("You found a safe key!");
                    inventory.Add("safe key");
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if ((x,y)==(4,5))
            {
                if (input=="check left drawer")
                {
                    Result("There's nothing in here.");
                }
                if (input == "check right drawer")
                {
                    if (!nailsFound)
                    {
                        Result("You found some nails.");
                        nailsFound = true;
                        inventory.Add("nails");
                    }
                    else
                    {
                        Result("There's nothing in here.");
                    }


                }
            }
            if ((x,y)==(4,6))
            {
                if (input == "check left drawer")
                {
                    Result("There's nothing in here.");
                }
                if (input == "check right drawer")
                {
                    Result("There's nothing in here.");
                }
            }
            if ((x,y)==(9,5))
            {
                if (!inshed)
                {

                    if (input == code.ToString() && padlock)
                    {

                        Result("You unlocked the shed");
                        padlock = false;
                    }
                    else if (input == code.ToString() && !padlock)
                    {
                        Result("The shed is already unlocked :P");
                    }
                    else if (!padlock && input == "enter shed")
                    {
                        Result("You entered the shed.");


                        Result("You stand within a toolshed.");
                        if (tools != null)
                        {

                            Result("The tools within it are:");

                            for (int i = 0; i < tools.Count; i++)
                            {
                                Result(tools[i]);
                            }
                        }
                        else
                        {
                            Result("No tools remain.");
                        }
                        inshed = true;
                    }
                    else
                    {
                        Result("You can't do that.");
                    }
                }
            }
            if (input == "exit shed")
            {
                if (inshed)
                {
                    inshed = false;
                    Move();
                }
                else
                {

                    Result("You are already out of the shed.");
                }
            }
            if (inshed)
            {

            }
            if (input == "grab ax")
            {
                if (inshed&&tools.Contains("ax"))
                {
                    inventory.Add("ax");
                    tools.Remove("ax");
                    Result("You got an ax.");
                }
                else
                {

                    Result("You can't do that");
                }
            }
            if (input=="fix bridge"||input=="repair bridge")
            {
                if (!locations.Contains((10,15)))
                {

                    if (inventory.Contains("nails") && inventory.Contains("hammer") && inventory.Contains("wood"))
                    {
                        if ((x, y) == (10, 14))
                        {
                            Result("The bridge is repaired.");
                            locations.Add((10, 15));
                        }
                        else
                        {
                            Result("You can't do that");
                        }
                    }
                    else
                    {
                        Result("You don't have enough supplies to repair the bridge.");
                    }
                }
                else
                {

                    Result("The bridge is already repaired.");
                }
            }
            if (input=="grab shovel")
            {
                if (Loc(13,21)&&!inventory.Contains("shovel"))
                {
                    inventory.Add("shovel");
                    Result("You got a shovel.");
                }
                else
                {
                    Result("You can't do that.");
                }
            }
            if (input=="grab hammer")
            {
                if (inshed)
                {
                    if (tools.Contains("hammer"))
                    {
                        Result("You got a hammer");
                        inventory.Add("hammer");
                        tools.Remove("hammer");
                    }
                    else
                    {
                        Result("You already took the hammer.");
                    }
                }
                else
                {
                    Result("You can't do that. ");
                }
            }
            if (Loc(14,3))
            {
                if (input=="chop rope"&&inventory.Contains("ax"))
                {
                    boatrope = false;
                    locations.Add((14, 2));
                    Result("You can now use the boat.");
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input=="fish")
            {
                if ((x, y) == (13, 3))
                {
                    Result("You got nothing. Try the other side of the lake.");
                }
                else if ((x, y) == (14,1))
                {
                    if (!padlockKey)
                    {
                        padlockKey = true;
                        inventory.Add("padlock key");
                        Result("You got a big one: a bulky old key.");
                    }
                    else
                    {
                        Result("There's nothing more to catch.");
                    }
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input== "chop down tree")
            {
                if (!inventory.Contains("wood") && new List<(int,int)>() { (6, 6), (6, 7), (6, 8), (7, 6), (7, 7), (7, 8), (8, 6), (8, 7), (8, 8) }.Contains((x, y)))
                {
                    inventory.Add("wood");
                    Result("You got wood.");
                }
                else if (inventory.Contains("wood"))
                {
                    Result("You already have wood.");
                }
                else
                {
                    Result("Tree? What tree?");
                }
            }
            if (input=="unlock gate")
            {
                if ((x,y)==(10,2))
                {
                    if(inventory.Contains("padlock key"))
                    {
                        locations.Add((10, 1));
                        Debug.Log("You unlocked the gate.");
                    }
                    else
                    {
                        Debug.Log("You need a padlock key.");
                    }
                }
            }
            if (input =="dig lump")
            {
                if ((x,y)==(16,6)&&inventory.Contains("shovel"))
                {
                    if (!inventory.Contains("rope"))
                    {

                        Result("You got rope");
                        inventory.Add("rope");
                    }
                    else
                    {
                        Result("You already dug it.");
                    }
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input=="grab shovel")
            {
                if (Loc(13,21)&&!inventory.Contains("shovel"))
                {
                    inventory.Add("shovel");
                }
            }
            if (input=="swing on rope")
            {
                if (inventory.Contains("rope"))
                {

                    if (Loc(9, 21))
                    {
                        x = 7;
                        y = 21;
                        Move();
                    }
                    else if(Loc(7,21))
                    {

                        x = 9;
                        y = 21;
                        Move();
                    }
                }
                else
                {
                    Result("You can't do that.");
                }
            }
            if (input=="grab pickaxe")
            {
                if ((x,y)==(10,-1)&&!pickaxefound)
                {
                    Result("You got the pickaxe");
                    inventory.Add("pickaxe");
                    pickaxefound = true;
                }
                else
                {

                    Result("You can't do that");
                }
            }
            if (input=="grab fishing rod")
            {
                if (inshed && !inventory.Contains("fishing rod"))
                {
                    Result("You got a fishing rod.");
                    inventory.Add("fishing rod");
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input=="strike ore")
            {
                if (Loc(9,21)&&inventory.Contains("pickaxe"))
                {
                    Result("You got a diamond!!!!");
                    inventory.Add("diamond");
                    Result("You win!!!!!");
                }
                else
                {
                    Result("You can't do that");
                }
            }
            if (input != code.ToString())
            {

                if (!new List<string>() {"strike ore","grab shovel", "swing on rope","dig lump","grab pickaxe","unlock gate","fish", "look under rock", "drop", "cut vines", "look under carpet", "enter shed", "exit shed", "check chandelier", "open safe", "grab ax","grab fishing rod", "grab hammer", "repair bridge","fix bridge","chop down tree","check left drawer", "check right drawer","chop rope","open chest" }.Contains(input))
                {
                    Result("You can't do that.");
                }
            }
        }
    }
    // Update is called once per frame
    void Move()
    {
        if (Loc(10,-1))
        {
            Result("You are within a spartan shack.");
            Result("The interior is unsightly, what with broken furniture abounding and torn walls.");
            Result("You notice an old pickaxe which shows its age but seems feasible.");
        }
        if (Loc(10,0))
        {
            Result("You stand before a dilapidated shack.");
        }
        if (Loc(10,1))
        {
            Result("You walk onto a gravelly path in a spartan bare plain.");
        }
        if (Loc(10, 2))
        {
            Result("You have reached an old rusty gate. A bulky padlock leaves it tightly sealed.");
        }
        if (Loc(10, 3))
        {
            Result("The old dusty road.");
        }
        if (Loc(10, 4))
        {
            Result("The old dusty road.");
        }
        if (Loc(10, 5))
        {
            Result("The old dusty road.");
        }
        if (Loc(10, 6))
        {
            Result("You have come to a fork in the road");
            Result("To the left is a cobblestone path, to the right is a dense, dark forest. The road continues normally ahead.");
        }
        if (Loc(11, 6))
        {
            Result("You are in a dense dark forest.");
        }
        if (Loc(12, 6))
        {
            Result("The dense dark woods.");
        }
        if (Loc(13, 6))
        {
            Result("There is a fork in the path going left and forward.");
        }
        if (Loc(14,6))
        {
            Result("The dense dark woods.");
        }
        if (Loc(15,6))
        {
            Result("The dense dark woods.");
        }
        if (Loc(16,6))
        {
            Result("You walk into a clearing in the center of which is a noticable lump in the ground.");
        }
        if (Loc(13, 5))
        {
            Result("The dense dark woods.");
        }
        if (Loc(13, 4))
        {
            Result("The dense dark woods.");
        }
        if (Loc(13, 3))
        {
            Result("You come into a clearing upon a small lake.");
        }
        if (Loc(14, 3))
        {
            Result("You come upon a boat port with an old rowboat tied to a post by a thick strong rope.");
        }
        if (Loc(14, 2))
        {
            Result("On the boat, you are within the lake.");
        }
        if (Loc(14, 1))
        {
            Result("You are at the other side of the lake.");
        }
        if (Loc(15, 1))
        {
            Result("The lakeside.");
        }
        if (Loc(16,1))
        {
            Result("There is a waterfall in the distance.");
        }
        if (Loc(17, 1))
        {
            Result("You are in front of a waterfall.");
        }
        if (Loc(18,1))
        {
            Result("You enter the waterfall and discover a secret area.");
            Result("There is a small chest.");
        }
        if (Loc(10, 7))
        {
            Result("The old dusty road");
        }
        if (Loc(10, 8))
        {
            Result("The old dusty road");
        }
        if (Loc(10, 9))
        {
            Result("The old dusty road");
        }
        if (Loc(10, 10))
        {
            Result("The old dusty road");
        }
        if (Loc(10, 11))
        {
            Result("The old dusty road");
        }
        if (Loc(10, 12))
        {
            Result("The old dusty road");
        }
        if (Loc(10, 13))
        {
            Result("The old dusty road");
        }
        if (Loc(10, 14))
        {

            if (!locations.Contains((10,15)))
            {

                Result("You find yourself in front of a dilapidated old bridge over a vigorous broad river.");
                Result("There are some step boards missing. It's not able to be crossed.");
            }
            else
            {
                Result("The old bridge is before you.");
            }
        }
        if (Loc(10, 15))
        {

            Result("You are on the bridge.");
        }
        if (Loc(10, 16))
        {
            Result("The old dusty road.");
        }
        if (Loc(10, 17))
        {
            Result("The old dusty road. The road goes left of you and ahead.");
        }
        if (Loc(10,18))
        {
            Result("You are at the mouth of a cave");
        }
        if (Loc(10, 19))
        {
            Result("You are within a dark cave.");
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
        }
        if (Loc(10, 20))
        {
            Result("You are within a dark cave.");
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
        }
        if (Loc(10, 21))
        {
            Result("You are within a dark cave.");
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
            else
            {
                Result("The cave continues ahead and left and right of you.");
            }
        }
        if (Loc(11, 21))
        {
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
            else
            {
                Result("The cave.");
                Result("Fool's gold sparkles on the walls.");
            }
        }
        if (Loc(12, 21))
        {
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
            else
            {
                Result("The cave.");
                Result("Green stactalites hang from the ceiling.");
            }
        }
        if (Loc(13, 21))
        {
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
            else
            {
                Result("You reach a dead end");
                Result("There is a shovel leaning against the wall");
            }
        }
        if (Loc(9, 21))
        {
            Result("You are within a dark cave.");
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
            else
            {
                Result("There is an abyss in front of you.");
            }
        }
        if (Loc(8,21))
        {
            Result("You fell down an abyss into a rapid");
            Result("You fell out of a waterfall and floated to the shore of a lake.");
            x = 13;
            y = 3;
            Move();
        }
        if (Loc(7, 21))
        {
            Result("You are within a dark cave.");
            if (!inventory.Contains("lantern"))
            {
                Result("You cannot see anything.");
            }
            else
            {
                Result("You stand before a shiny ore!");
            }
        }
        if (Loc(9, 6))
        {
            Result("You walk along a cobblestone path between old shrubs and shady birch trees.");
            Result("A cracked old cobblestone wall, on which hangs many vines, runs parallel to the path behind the shrubs.");
        }
        if (Loc(8, 6))
        {
            Result("There is an old door with a ring handle in the wall to the right of you.");

        }
        if (Loc(8,5))
        {
            Result("You step within a beautiful garden filled with poppies, roses, and daisies.");
            Result("The path continues left and right of you.");
        }
        if (Loc(7, 5))
        {
            Result("There is a group of lilies of green, blue, and red.");
            Result("A mockingbird sings atop the wall.");
            Result("The path ends in front of the wall covered in thick, tree-like vine branches. ");
        }
        if (Loc(9, 5))
        {
            Result("You stand in front of a tool shed, locked by a padlock requiring a numerical, four-digit code.");
        }
        if (Loc(7, 6))
        {
            Result("There is a forest of trees to the left of you. The cobblestone path leads to a small, desolate cottage.");

        }
        if (Loc(6, 7))
        {
            Result("You stand within a forest of birch trees.");
            Result("The grass grows long and is littered with beautiful leaves");
            Result("Blue jays feed their young in their nests");
        }
        if (Loc(6, 8))
        {
            Result("You stand within a forest of birch trees.");
            Result("The grass grows long and is littered with beautiful leaves");
            Result("Blue jays feed their young in their nests");
        }
        if (Loc(7, 7))
        {
            Result("You stand within a forest of birch trees.");
        }
        if (Loc(7, 8))
        {
            Result("You stand within a forest of birch trees.");
            Result("Mockingbirds sing dazzling melodies.");
        }
        if (Loc(8, 7))
        {
            Result("You stand within a forest of birch trees.");
            Result("Sap runs down cracks in the trunks.");
            Result("Skunks frolick in the distance.");
            Result("Crickets' songs fill the air.");
        }
        if (Loc(8, 8))
        {
            Result("You stand within a forest of birch trees.");
            Result("The soothing zephyr sways the branches.");
            Result("A porcupine walks slowly in the background.");
            Result("The sun's shining rays penetrate the branches and cast upon a large rock.");
        }
        if (Loc(6, 6))
        {
            Result("You stand before the porch of an old white cottage.");

        }
        if (Loc(5, 6))
        {
            Result("You are on the porch of the old white cottage.");
            Result("A swinging couch suspended by ropes from the ceiling hangs to the right of you.");
            Result("The front door lies in front of you.");
        }
        if (Loc(4, 6))
        {
            Result("You find yourself in an old living room with surprisingly well-preserved though somewhat aged furniture.");
            Result("There is a couch in one corner of the room with a stand-up cylindrical-headed lamp next to it with an");
            Result("attractive blue color and zig-zag pattern. In the left corner of the room is an attractively carved");
            Result("sideboard with two drawers, two lower compartment doors, and an old book on its built-in shelf above them.");
            Result("On the creaky wooden floor is a rather attractive rug with an intricately sown, vague, colorful pattern.");

        }
        if (Loc(4, 5))
        {
            Result("You walk into a fancy dining room with ornate chairs and tables, a sideboard with two drawers.");
            Result("A chandelier hangs over the broad table on which lies a beautiful velvet tablecloth with a vase on it.");

            Result("A staircase lies at the furthest end of the room.");
        }
        if (Loc(4, 4))
        {

            Result("You walk upstairs and find yourself in the master bedroom.");
            Result("A grand bed for two, behind which lies a broad window, lies in the center of the wall left of you");
            Result("A safe lies at the other end of the room");
        }
    }
}
