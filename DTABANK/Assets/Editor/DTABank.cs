using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DTABank : EditorWindow {
    Scene curscene;
    [MenuItem("Window/Open DTABank")]
    public static void ShowWindow () {
        GetWindow<DTABank>("DTABank");
    }
    void OnGUI()
    {
        curscene = SceneManager.GetActiveScene();
        if (!EditorApplication.isPlaying && curscene.name != "Home")
        {
            Debug.LogWarning("Please start the scene the web manager is located");
            return;
        }
        EditorGUILayout.LabelField("Button", EditorStyles.boldLabel);
        if (GUILayout.Button("Loading Quests"))
        {
            Web_Manager.instance.LoadQuests();
        }
        EditorGUILayout.LabelField("Quests", EditorStyles.boldLabel);
        foreach (Quest quest in Event_Manager.Get_Quests())
        {
            GUILayout.Label("Quest: " + quest.name);
            
        }
    }
	
}
