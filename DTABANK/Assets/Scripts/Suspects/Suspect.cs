
/* Suspect:
 * Has sprites for hair, face, clothing
 * Is a culprit or not
 */

[System.Serializable]
public class Suspect : Base_Suspect {
    /*public string name;
    public string description;
    public string look;
    public float height;*/

    //public Suspect GetSuspect() {
    //    return suspect;
    //}

    public Suspect(string n, string d, string l, float h) : base(n, d, l, h) {
    }
}
