using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_controller : MonoBehaviour
{
    public GameObject[] units;
    private Color opponent_Color = new Color(250.0f,161.0f,27.0f, 186.0f);
    private Color player_Color = new Color(103.0f, 255.0f, 76.0f, 158.0f);
    private Color default_Color = new Color(255.0f, 255.0f, 255.0f, 100.0f);

    private bool dot = false;
    private bool show = true;

    private int chance = 0;

    private int[] pos = { 0, 0};
    private int j = 0;

    // Start is called before the first frame update
    void Start()
    {
       for(int i=0;i<units.Length;i++)
        {
            units[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
            RectTransform size = units[i].GetComponentInChildren<Text>().GetComponent<RectTransform>();
            size.localScale = new Vector3(0.16f, 0.16f, 0.16f);

            units[i].GetComponentInChildren<Text>().resizeTextForBestFit = true;
        }
    }
    int generate_random_number()
    {
        return UnityEngine.Random.Range(2, 12);
    }

    void play()
    {
        //reset original color
        set_color(pos[chance], default_Color);

        j = generate_random_number();
        show_dice(j, chance);

        //Update pos of player
        j = pos[chance] + j;
        pos[chance] = j;

        //Set current visited cell to respective color.
        if (chance == 0)
            set_color(j, player_Color);
        else
            set_color(j, opponent_Color);

        if(j ==0)
            Debug.Log("Player:" + units[pos[0]].GetComponentInChildren<Text>().text + " " +
            "Opponent:" + units[pos[1]].GetComponentInChildren<Text>().text);

        else if (chance == 0)
            Debug.Log("Player:" + units[pos[0]].GetComponentInChildren<Text>().text);
        else
            Debug.Log("Opponent:" + units[pos[1]].GetComponentInChildren<Text>().text);
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
}
