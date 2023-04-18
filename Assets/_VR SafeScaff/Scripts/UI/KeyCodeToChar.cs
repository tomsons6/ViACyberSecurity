using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeToChar : MonoBehaviour
{
    public static Dictionary<KeyCode, string> KeyCodeDictionary = new Dictionary<KeyCode, string>()
    {
      //Lower Case Letters
      { KeyCode.A,"A"},
      { KeyCode.B, "B"},
      { KeyCode.C,"C"},
      { KeyCode.D,"D"},
      { KeyCode.E,"E"},
      { KeyCode.F,"F"},
      { KeyCode.G,"G"},
      { KeyCode.H,"H"},
      { KeyCode.I,"I"},
      { KeyCode.J,"J"},
      { KeyCode.K,"K"},
      { KeyCode.L,"L"},
      { KeyCode.M,"M"},
      { KeyCode.N,"N"},
      { KeyCode.O,"O"},
      { KeyCode.P,"P"},
      { KeyCode.Q,"Q"},
      { KeyCode.R,"R"},
      { KeyCode.S,"S"},
      { KeyCode.T,"T"},
      { KeyCode.U,"U"},
      { KeyCode.V,"V"},
      { KeyCode.W,"W"},
      { KeyCode.X,"X"},
      { KeyCode.Y,"Y"},
      { KeyCode.Z,"Z"},
  
      //KeyPad Numbers
      { KeyCode.Keypad1,"1"},
      { KeyCode.Keypad2,"2"},
      { KeyCode.Keypad3,"3"},
      { KeyCode.Keypad4,"4"},
      { KeyCode.Keypad5,"5"},
      { KeyCode.Keypad6,"6"},
      { KeyCode.Keypad7,"7"},
      { KeyCode.Keypad8,"8"},
      { KeyCode.Keypad9,"9"},
      { KeyCode.Keypad0,"0"},

      // Alpha numbers
      { KeyCode.Alpha1,"1"},
      { KeyCode.Alpha2,"2"},
      { KeyCode.Alpha3,"3"},
      { KeyCode.Alpha4,"4"},
      { KeyCode.Alpha5,"5"},
      { KeyCode.Alpha6,"6"},
      { KeyCode.Alpha7,"7"},
      { KeyCode.Alpha8,"8"},
      { KeyCode.Alpha9,"9"},
      { KeyCode.Alpha0,"0"},

      { KeyCode.Comma,","},
      { KeyCode.Period,"."},
      { KeyCode.Space," "}
    };

    public static string GetChar(KeyCode keycode)
    {
        string output;
        if (KeyCodeDictionary.TryGetValue(keycode, out output))
        {
            return output;
        }
        else
        {
            return " ";
        }

    }
}
