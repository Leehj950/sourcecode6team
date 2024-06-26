using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public GameObject JournalCanvas;
    public TextMeshProUGUI Text;
    public Sprite paperImage;

    
    
    public void ActiveJournal()
    {
        JournalCanvas.SetActive(true);
    }

    public void CloseJournal()
    {
        paperImage = null;
        Text.text = "";
        JournalCanvas.SetActive(false);

    }

}
