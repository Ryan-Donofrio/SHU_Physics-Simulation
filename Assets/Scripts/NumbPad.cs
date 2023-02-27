using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumbPad : MonoBehaviour
{

    public TMP_Text textComponent;
    public TMP_Text textComponentTwo;
    public TMP_Text textComponentThree;
    private string number;
    private string numberTwo;
    private string numberThree;
    private bool buttonOne;
    private bool buttonTwo;
    private bool buttonThree;
    public GameObject selectedOne;
    public GameObject selectedTwo;
    public GameObject selectedThree;

    public TMP_Text outputText;
    

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        buttonOne = true;
        selectedOne.SetActive(true);
        selectedTwo.SetActive(false);
        selectedThree.SetActive(false);
    }
    public void ButtonOnePress()
    {
        buttonOne = true;
        buttonTwo = false;
        selectedOne.SetActive(true);
        selectedTwo.SetActive(false);
        selectedThree.SetActive(false);
    }
    public void ButtonTwoPress()
    {
        buttonOne = false;
        buttonTwo = true;
        selectedTwo.SetActive(true);
        selectedOne.SetActive(false);
        selectedThree.SetActive(false);
    }
    public void ButtonThreePress()
    {
        buttonOne = false;
        buttonTwo = false;
        buttonThree = true;
        selectedOne.SetActive(false);
        selectedTwo.SetActive(false);
        selectedThree.SetActive(true);
    }
    public void Input(int num)
    {
        if (buttonOne == true)
        {
            number = number + num;
            textComponent.text = number;
        }
        else if (buttonTwo == true)
        {
            numberTwo = numberTwo + num;
            textComponentTwo.text = numberTwo;
        }
        else if (buttonThree == true)
        {
            numberThree = numberThree + num;
            textComponentThree.text = numberThree;
        }
        Debug.Log("Input" + number);
    }
    public void Punctuation(string punc)
    {
        if (buttonOne == true)
        {
            number = number + punc;
            textComponent.text = number;
        }
        else if (buttonTwo == true)
        {
            numberTwo = numberTwo + punc;
            textComponentTwo.text = numberTwo;
        }
        else if (buttonThree == true)
        {
            numberThree = numberThree + punc;
            textComponentThree.text = numberThree;
        }
        Debug.Log("Punctuation" + number);
    }
    public void Clear()
    {
        //if (buttonOne == true)
        //{
        //    number = "";
        //    textComponent.text = number;
        //}
        //else if (buttonTwo == true)
        //{
        //    numberTwo = "";
        //    textComponentTwo.text = numberTwo;
        //}
        //else if (buttonThree == true)
        //{
        //    numberThree = "";
        //    textComponentThree.text = numberThree;
        //}

        number = "";
        textComponent.text = number;
        numberTwo = "";
        textComponentTwo.text = numberTwo;
        numberThree = "";
        textComponentThree.text = numberThree;
    }
    public void Test()
    {
        if (buttonOne == true)
        {
            //number = "Testing...";
            textComponent.text = number;
        }
        else if (buttonTwo == true)
        {
            //numberTwo = "Testing...";
            textComponentTwo.text = numberTwo;
        }
        else if (buttonThree == true)
        {
            //numberThree = "Testing...";
            textComponentThree.text = numberThree;
        }
        OutputInfo();
    }
    public void Delete()
    {
        if (buttonOne == true)
        {
            number = number.Substring(0, number.Length - 1);
            textComponent.text = number;
        }
        else if (buttonTwo == true)
        {
            numberTwo = numberTwo.Substring(0, numberTwo.Length - 1);
            textComponentTwo.text = numberTwo;
        }
        else if (buttonThree == true)
        {
            numberThree = numberThree.Substring(0, numberThree.Length - 1);
            textComponentThree.text = numberThree;
        }
    }

    public void OutputInfo()
    {
        outputText.text = "S = " + number + " + " + numberTwo + " * " + numberThree;
    }
}
