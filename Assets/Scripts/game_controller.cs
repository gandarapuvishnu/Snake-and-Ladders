using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_controller : MonoBehaviour
{
    public GameObject[] units;
    private Color opponent_Color = new Color(0.9803922f, 0.6313726f, 0.1058823f, 0.7294118f);
    private Color player_Color = new Color(0.4039216f, 1.0f, 0.2980392f, 0.6196079f);
    private Color default_Color = new Color(1.0f, 1.0f, 1.0f, 0.3921569f);

    private bool dot;
    private bool show;

    private int chance = 0;

    private int[] pos = { 0, 0 };
    private int j;
    private bool check;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }
    void Reset()
    {
        pos[0] = pos[1] = 0;
        j = 0;
        show_dice(j, 0);show_dice(j, 1);

        check = false;

        dot = false;
        show = true;

        for (int i = 0; i < units.Length; i++)
        {
            units[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
            RectTransform size = units[i].GetComponentInChildren<Text>().GetComponent<RectTransform>();
            size.localScale = new Vector3(0.16f, 0.16f, 0.16f);
            set_color(i, default_Color);
            units[i].GetComponentInChildren<Text>().resizeTextForBestFit = true;
        }
    }
    int generate_random_number()
    {
        return Random.Range(2, 12);
    }

    void play()
    {
        //reset original color
        set_color(pos[chance], default_Color);
        //reset info log
        show_info("");

        j = generate_random_number();
        Debug.Log("Random: " + j);
        show_dice(j, chance);

        //Update pos of player 
        j = pos[chance] + j;
        if (ladder_at(j) != -1)
        {
            j = ladder_at(j);
            if (chance == 0)
                show_info("Player enhanced to: " + (j+1));
            else
                show_info("Opponent enhanced to: " + (j+1));
        }
        else if(snake_at(j) != -1)
        {
            j = snake_at(j);
            if (chance == 0)
                show_info("Player swallowed to: " + (j+1));
            else
                show_info("Opponent swallowed to: " + (j+1));
        }            

        //Set current visited cell to respective color.
        if(j >= 99)
            check = true;
        else if (chance == 0)
            set_color(j, player_Color);
        else if(chance == 1)
            set_color(j, opponent_Color);

        pos[chance] = j;

        if (chance == 0)
            Debug.Log("Player: " + (pos[0]+1));
        else
            Debug.Log("Opponent: " + (pos[1] + 1));

        //Check for win
        if(check)
        {
            if (pos[0] >= 99)
            {
                chance = 0;
                Reset();
                show_info("Player has won.");
            }
            else if (pos[1] >= 99)
            {
                chance = 1;
                Reset();
                show_info("Opponent has won.");
            }
        }
        chance += 1;
        chance %= 2;

    }

    // Update is called once per frame
    void Update()
    {
        if (dot == true)
        {
            play();

            dot = false;
            show = true;
        }
        else
        {
            if (show == true)
            {
                Debug.Log("Press space to continue");
                show = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {                
                dot = true;
            }
        }
    }

    void show_info(string message)
    {
        Debug.Log(message);
        GameObject.Find("Info").GetComponentInChildren<Text>().text = message;
    }

    void show_dice(int number, int chance)
    {
        string curr_dicer;
        if (chance == 0)
            curr_dicer = "player_dice";
        else
            curr_dicer = "opponent_dice";
        GameObject.Find(curr_dicer).GetComponentInChildren<Text>().text = number.ToString();
    }

    void set_color(int curr_index, Color clr)
    {
        units[curr_index].GetComponent<Image>().color = clr;
    }

    int ladder_at(int x)
    {
        if (x == 3)
            return 44;
        else if (x == 5)
            return 24;
        else if (x == 39)
            return 76;
        else if (x == 46)
            return 65;
        else if (x == 49)
            return 92;
        else if (x == 67)
            return 86;
        else if (x == 60)
            return 97;
        else
            return -1;
    }
    int snake_at(int y)
    {
        if (y == 42)
            return 16;
        else if (y == 61)
            return 21;
        else if (y == 90)
            return 50;
        else if (y == 98)
            return 9;
        else if (y == 31)
            return 4;
        else if (y == 51)
            return 10;
        else if (y == 56)
            return 23;
        else if (y == 94)
            return 75;
        else
            return -1;
    }
}
