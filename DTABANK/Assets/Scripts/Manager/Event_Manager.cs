using UnityEngine;
using System.Collections.Generic;

public class Event_Manager : MonoBehaviour {

    //Delegate to load in objects
    public delegate void LoadObjects();
    public static event LoadObjects LoadQuest;

    //Delegate to draw in objects
    public delegate void DrawObjects();
    public static event DrawObjects DrawQuests;

    //Delegate to draw in objects
    public delegate void LockGame();
    public static event LockGame GameLock;

    //Delegate to add Quest to the QuestManager;
    public delegate void QuestData(Quest quest);
    public static event QuestData AddQuest;
    public static event QuestData AddQuestMarker;
    public static event QuestData AddQuestCircles;

    //Delegate to Toggle Objects
    public delegate void ToggleObjects(bool toggle, int name);
    public static event ToggleObjects ToggleDialog;

    //Set Quests in the quest manager
    public delegate void QuestListSet(System.Collections.Generic.List<Quest> ql);
    public static event QuestListSet SetQuestList;

    public delegate List<Quest> QuestGet();
    public static event QuestGet GetQuests;

    //Delegate to load in dialogs
    public delegate void Checks();
    public static event Checks DistanceCheck;

    public delegate void LoadDialog(Quest _quest, System.Collections.Generic.List<Suspect> _suspects);
    public static event LoadDialog DialogLoad;

    //Set current quest
    public delegate void CurrentQuestSet(Quest _q);
    public static event CurrentQuestSet SetCurrentQuest;
    public delegate void CurrentQuestClueSet(Quest_Clues _q);
    public static event CurrentQuestClueSet SetClueFound;


    //Set Quest clues
    public delegate void CluesQuestSet(List<Quest_Clues> QL);
    public static event CluesQuestSet SetCurrentQuestClues;
    public delegate List<Quest_Clues> CluesGet();
    public static event CluesGet GetClues;
    public static event CluesGet Get_LoadedClues;

    //Save and load the Quests
    public delegate void SaveAndLoad();
    public static event SaveAndLoad LoadQuestClues;
    public static event SaveAndLoad LoadQuestCluesF;
    public static event SaveAndLoad SaveQuestClues;

    //Operate ClueManager
    public delegate void CallClue();
    public static event CallClue CalculateClue;
    //Loads in Objects in the scene and deletes other markers or data
    public static void Load_Objects(LOAD_OBJECTS lo) {
        switch (lo) {
            case LOAD_OBJECTS.Quest:
                LoadQuest();
                break;
            case LOAD_OBJECTS.Userdata:
                break;
        }
        LoadQuest();
    }
    public static void Add_Quest(Quest quest) {
        AddQuest(quest);
    }

    public static void Draw_Quest(DRAW_OBJECTS d_o) {
        switch (d_o) {
            case DRAW_OBJECTS.Quest:
                //DrawQuests();
                break;
        }
    }
    public static void Game_Lock()
    {
        GameLock();
    }
    public static void Toggle_Elements(DRAW_OBJECTS d_o, bool toggle, int id) {
        switch (d_o) {
            case DRAW_OBJECTS.Dialog:
                ToggleDialog(toggle, id);
                break;
        }
    }
    public static void Dialog_Load(Quest _quest, System.Collections.Generic.List<Suspect> _suspects) {
        DialogLoad(_quest, _suspects);
    }
    public static void Add_QuestMarker(Quest quest) {
        AddQuestMarker(quest);
    }
    public static void Add_QuestCircles(Quest quest) {
        AddQuestCircles(quest);
    }
    public static void Set_ClueFound(Quest_Clues _quest) {
        SetClueFound(_quest);
    }
    public static void Calculate_Clue() {
        CalculateClue();
    }
    public static void Distance_Check() {
        //DistanceCheck();
    }
    public static void Set_QuestList(System.Collections.Generic.List<Quest> ql) {
        SetQuestList(ql);
    }
    public static void Set_CurrentQuest(Quest q)
    {
        SetCurrentQuest(q);
    }
    public static List<Quest_Clues> Get_Clues()
    {
        return GetClues();
    }
    public static void Set_CurrentQuestClues(List<Quest_Clues> QC)
    {
        SetCurrentQuestClues(QC);
    }
    public static List<Quest> Get_Quests()
    {
        return GetQuests();
    }
    public static List<Quest_Clues> Get_XML_Clues()
    {
        return Get_LoadedClues();
    }
    public static void Load_QuestCluesF()
    {
        LoadQuestCluesF();
    }
    public static void Load_QuestClues()
    {
        LoadQuestClues();
    }
    public static void Save_QuestClues()
    {
        SaveQuestClues();
    }
}
public enum LOAD_OBJECTS
{
    Quest,
    Userdata
}

public enum DRAW_OBJECTS {
    Quest,
    Userdata,
    Dialog
}