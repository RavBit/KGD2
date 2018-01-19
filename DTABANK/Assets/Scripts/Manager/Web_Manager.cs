using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using System;


public class Web_Manager : MonoBehaviour
{
    public static Web_Manager instance;
    public List<Quest> QuestData;
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one App Manager in the scene");
        else
            instance = this;
    }


    public IEnumerator LoadQuests()
    {
        QuestData.Clear();
        Debug.Log("Loading..");
        //Command gescheiden bestand CSV
        // ! https://www.mysql.com/products/workbench/
        WWW questdata = new WWW("http://81.169.177.181/UIB/request_quests.php");
        yield return questdata;
        Debug.Log(questdata.text);
        if (string.IsNullOrEmpty(questdata.error))
        {
            QuestData = JsonHelper.getJsonArray<Quest>(questdata.text).ToList<Quest>();
            for (int i = 0; i < QuestData.Count; i++)
            {
                WWWForm quest_id = new WWWForm();
                quest_id.AddField("quest_id", QuestData[i].id);
                WWW witnessdata = new WWW("http://81.169.177.181/UIB/request_witness.php", quest_id);
                yield return witnessdata;
                QuestData[i].witness = JsonHelper.getJsonArray<Witness>(witnessdata.text).ToList<Witness>();
                for (int j = 0; j < QuestData[i].witness.Count; j++)
                {
                    WWWForm witness_id = new WWWForm();
                    witness_id.AddField("witness_id", QuestData[i].witness[j].id);
                    WWW w = new WWW("http://81.169.177.181/UIB/request_dialogs.php", witness_id);
                    yield return w;
                    QuestData[i].witness[j].dialogs = JsonHelper.getJsonArray<Quest_BaseDialog>(w.text).ToList<Quest_BaseDialog>();
                }
                WWW suspectdata = new WWW("http://81.169.177.181/UIB/request_suspects.php", quest_id);
                yield return suspectdata;
                QuestData[i].Suspects = JsonHelper.getJsonArray<Suspect>(suspectdata.text).ToList<Suspect>();
                WWW cluesdata = new WWW("http://81.169.177.181/UIB/request_clues.php", quest_id);
                yield return cluesdata;
                Debug.Log("cluesdata " + cluesdata.text);
                QuestData[i].Clues = JsonHelper.getJsonArray<Quest_Clues>(cluesdata.text).ToList<Quest_Clues>();
            }
        }
        else
        {
            Debug.LogError("ERROR FATAL");
        }
        Event_Manager.Set_QuestList(QuestData);
        WWWForm q_d = new WWWForm();
        q_d.AddField("user_id", App_Manager.instance.User.id);
        //Command gescheiden bestand CSV
        // ! https://www.mysql.com/products/workbench/
        WWW qd = new WWW("http://81.169.177.181/UIB/get_quest.php", q_d);
        yield return qd;
        Debug.Log(qd.text);
        if (string.IsNullOrEmpty(qd.error))
        {
            CurStartQuestChecker CSQC = JsonUtility.FromJson<CurStartQuestChecker>(qd.text);
            if (CSQC.success)
            {
                foreach (Quest quest in QuestData)
                {
                    if (quest.id == CSQC.quest_id)
                    {
                        Event_Manager.Load_QuestClues();
                        Event_Manager.Set_CurrentQuest(quest);
                        List<Quest_Clues> QC = new List<Quest_Clues>();
                        QC = Event_Manager.Get_XML_Clues();
                        if (QC.Count > 0) {
                            Event_Manager.Set_CurrentQuestClues(QC);
                            Debug.Log("COUNT " +  QC.Count);
                          }
                        Debug.Log("CALCULATING CLUE");
                        //if(Quest_Manager.)
                        Event_Manager.Calculate_Clue();
                    }
                }
            }
            else
            {
                Debug.Log("No Quest started");
                Event_Manager.Draw_Quest(DRAW_OBJECTS.Quest);
            }
        }
        Event_Manager.Distance_Check();
    }
    public IEnumerator StartQuest(Quest CurQuest)
    {
        Debug.Log("Loading..");
        WWWForm quest_id = new WWWForm();
        quest_id.AddField("quest_id", CurQuest.id);
        quest_id.AddField("user_id", App_Manager.instance.User.id);
        //Command gescheiden bestand CSV
        // ! https://www.mysql.com/products/workbench/
        WWW questdata = new WWW("http://81.169.177.181/UIB/set_quest.php", quest_id);
        yield return questdata;
        Debug.Log(questdata.text);
        if (string.IsNullOrEmpty(questdata.error))
        {
            CurQuestChecker CQC = JsonUtility.FromJson<CurQuestChecker>(questdata.text);
            Debug.Log("CQC : " + CQC.success);
            if(CQC.success)
            {
                Debug.Log("Quest started");
                Event_Manager.Set_CurrentQuest(CurQuest);
                Event_Manager.Load_QuestCluesF();
            }
        }
        Quest_Manager.Load_Quest();

    }
    public void Cancel_Quest(Quest CurrentQuest)
    {
        StartCoroutine("CancelQuest", CurrentQuest.id);
    }
    public IEnumerator CancelQuest(int CurQuest)
    {
        WWWForm quest_id = new WWWForm();
        quest_id.AddField("quest_id", CurQuest);
        WWW questdata = new WWW("http://81.169.177.181/UIB/cancel_quest.php", quest_id);
        yield return questdata;
        if (string.IsNullOrEmpty(questdata.error))
        {
            CurQuestChecker CQC = JsonUtility.FromJson<CurQuestChecker>(questdata.text);
            Debug.Log("CANCELATION : " + CQC.success);
            if (CQC.success)
            {
                Debug.Log("Succesfull Canceled");
                Event_Manager.Set_CurrentQuest(null);
                StopCoroutine("LoadQuests");
                StartCoroutine("LoadQuests");
            }
            else
            {
                Debug.LogError("NO QUEST RUNNING");
            }
        }

    }
    public void EndQuest(int time)
    {
        StartCoroutine("End_Quest", time);
    }
    public IEnumerator End_Quest(int time)
    {
        Debug.Log("Loading..");
        WWWForm score_id = new WWWForm();
        score_id.AddField("user_id", App_Manager.instance.User.id);
        score_id.AddField("score_id", time);
        //Command gescheiden bestand CSV
        // ! https://www.mysql.com/products/workbench/
        WWW scoredata = new WWW("http://81.169.177.181/UIB/set_score.php", score_id);
        yield return scoredata;
        Debug.Log(scoredata.text);
        if (string.IsNullOrEmpty(scoredata.error))
        {
            CurQuestChecker CQCX = JsonUtility.FromJson<CurQuestChecker>(scoredata.text);
            if (CQCX.success)
            {
                Debug.Log("Quest finished");
            }
        }
        Debug.Log("Quest ended");
    }
}

public class CurQuestChecker
{
    public bool success;
    public string error;
}
public class CurStartQuestChecker
{
    public bool success;
    public string error;
    public int quest_id;
}