using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManager : MonoBehaviour
{

    public TMP_InputField userName;
    public TMP_InputField roomName;
    private string userNameLetter;
    private string roomNameLetter;
    private bool userNameSelected;
    private bool roomNameSelected;

    public void inputFieldSelected(int ayesorno)
    {
        if (ayesorno == 0)
        {
            userNameSelected = true;
            roomNameSelected = false;
            Debug.Log("Userselected");
        }
        else if (ayesorno == 1)
        {
            userNameSelected = false;
            roomNameSelected = true;
        }
        else
        {
            Debug.Log("Error Input feild selection ");
        }
    }
    
    public void Input(string letter)
    {
       if (userNameSelected == true)
        {
            userName.text += letter;
            Debug.Log("Pushed letter");
        }
       else if (roomName == true)
        {
            roomName.text += letter;
        }
       else
        {
            Debug.Log("Error in Input function finding bool selection");
        }
    }
    
    public void Clear()
    {
        if (userNameSelected == true)
        {
            userName.text = "";
        }
        else if (roomName == true)
        {
            roomName.text = "";
        }
        else
        {
            Debug.Log("Error in clear function");
        }
    }
    
    public void Delete()
    {
        if (userNameSelected == true)
        {
            userName.text = userName.text.Substring(0, userName.text.Length - 1);
        }
        else if (roomName == true)
        {
            roomName.text = roomName.text.Substring(0, roomName.text.Length - 1);
        }
        else
        {
            Debug.Log("Delete Error");
        }
    }

}
