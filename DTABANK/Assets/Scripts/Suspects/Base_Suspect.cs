using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Base_Suspect {
    public string name;
    public string description;
    public string look;
    public float height;
    private GameObject suspectParent;

    public SpriteRenderer hairRenderer;
    public SpriteRenderer faceRenderer;
    public SpriteRenderer clothesRenderer;
    public GameObject suspectBody;
    public GameObject suspectHair;
    private SpriteRenderer bodySR;
    private SpriteRenderer hairSR;
    private bool isCulprit;
    private char[] lookArray;

    public Base_Suspect(string n, string d, string l, float h) {
        name = n;
        description = d;
        look = l;
        height = h;
        lookArray = look.ToCharArray();


        suspectBody = new GameObject();
        suspectParent = GameObject.FindWithTag("SuspectParent");
        suspectBody.transform.SetParent(suspectParent.transform);
        suspectBody.name = name;
        bodySR = suspectBody.AddComponent<SpriteRenderer>();
        suspectHair = new GameObject();
        suspectHair.name = name + " hair";
        suspectHair.transform.SetParent(suspectBody.transform);
        hairSR = suspectHair.AddComponent<SpriteRenderer>();

        Sprite body;
        Sprite hair;

        if (lookArray[0] == 'A' && lookArray[1] == 'A') {
            body = Resources.Load<Sprite>("Sprites/Body/Male");
        } else {
            body = Resources.Load<Sprite>("Sprites/Body/Female");
        }
        bodySR.sprite = body;
        
        if (lookArray[2] == 'a') {
            hair = Resources.Load<Sprite>("Sprites/Hair/Square");
        } else {
            hair = Resources.Load<Sprite>("Sprites/Hair/Triangle");
        }
        hairSR.sprite = hair;

    }
    

    public void SetHairColor(Color color) {
        hairRenderer.color = color;
    }

    public void SetFaceColor(Color color) {
        faceRenderer.color = color;
    }

    public void SetClothesColor(Color color) {
        clothesRenderer.color = color;
    }
}
