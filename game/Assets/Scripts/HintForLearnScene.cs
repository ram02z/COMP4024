using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintForLearnScene : Hint
{

    public void PlayHint()
    {
        string currentFrenchWord = GetVisibleFrenchText();

        if (currentFrenchWord != null)
        {
            PlayHint(currentFrenchWord);
        }
    }

    private string GetVisibleFrenchText()
    {
        GameObject canvasObject = GameObject.Find("Canvas");
        TextMeshProUGUI[] textMeshProComponents = canvasObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        string targetTextMeshProName = "FrenchWord";
        string visibleText = null;


        foreach (TextMeshProUGUI textMeshPro in textMeshProComponents)
        {
            if (textMeshPro.gameObject.name == targetTextMeshProName)
            {
                visibleText = textMeshPro.text;
            }
        }

        return visibleText;
    }

    // Update is called once per frame
    public override void Update()
    {

    }
}
