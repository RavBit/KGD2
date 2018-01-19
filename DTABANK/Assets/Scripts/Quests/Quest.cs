using System.Collections.Generic;

[System.Serializable]
public class Quest {
    public int id;
    public string name;
    public string description;
    public float start_x;
    public float start_y;
    public int curdialog;
    public List<Witness> witness;
    public bool ClickAble;
    public List<Suspect> Suspects;
    public List<Quest_Clues> Clues;
} 
