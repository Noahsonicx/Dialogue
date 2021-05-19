using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager theManager;

    Dialogue loadedDialogue;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform dialogueButtonPanel;
    [SerializeField] Text responseText;

    

    private void Awake()
    {
        if (theManager == null)
        {
            theManager = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void LoadDialogue(Dialogue dialogue)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        loadedDialogue = dialogue;

        ClearButtons();
        int index = 0;
        Button spawnedButton;
        foreach (LineOfDialogue data in dialogue.dialogueOptions)
        {
            float? currentApproval = FactionManager.instance.FactionsApproval(loadedDialogue.faction);
                if (currentApproval != null && currentApproval > data.minApproval)
            {


                spawnedButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
                spawnedButton.GetComponentInChildren<Text>().text = data.question;

                // i2 will be a differnt instance next loop.
                // it will be "this" instance of i2 but if just i "ButtonPressed(i)" they will all reference the same thing.
                data.buttonID = index;

                // delegate is a variable that acts like a fuction, button the function can change depending on different factors.
                spawnedButton.onClick.AddListener(() => ButtonPressed(data.buttonID));
            }    
            
            index++;
        }

        spawnedButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
        spawnedButton.GetComponentInChildren<Text>().text = dialogue.goodbye.question;
        dialogue.goodbye.buttonID = index;
        spawnedButton.onClick.AddListener(() =>EndConversation());

        //dialogue.goodbye.buttonID = index;
        // delegate is a variable that acts like a fuction, button the function can change depending on different factors.
        


        DisplayResponse(loadedDialogue.greeting);
    }

    void EndConversation()
    {
        ClearButtons();
        DisplayResponse(loadedDialogue.goodbye.response);

        if(loadedDialogue.goodbye.nextDialogue != null)
        {
            loadedDialogue = loadedDialogue.goodbye.nextDialogue;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true); 
        }
    }

    void ButtonPressed(int index)
    {
        FactionManager.instance.FactionsApproval(loadedDialogue.faction,
            loadedDialogue.dialogueOptions[index].changeApproval);

        if (loadedDialogue.dialogueOptions[index].nextDialogue != null)
        {
            LoadDialogue(loadedDialogue.dialogueOptions[index].nextDialogue);
        }

        else {
            DisplayResponse(loadedDialogue.dialogueOptions[index].response);

        }
    }


    private void DisplayResponse(string response)
    {
        responseText.text = response;
    }
    
    
    void ClearButtons()
    {
        foreach (Transform child in dialogueButtonPanel)
        {
            Destroy(child.gameObject);
        }
    }




}