using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyButtonSound : MonoBehaviour
{

    [SerializeField] private List<Button> allButtons;

    private void Start()
    {
        allButtons.AddRange(GetComponentsInChildren<Button>());

        foreach(Button b in allButtons)
        {
            b.onClick.AddListener(PlayClickSound);
        }
    }

    public void PlayClickSound()
    {
        SoundManager.GetSoundManager.PressMenuButton();
    }
}
