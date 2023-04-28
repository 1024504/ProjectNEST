using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtitlesManager : MonoBehaviour
{
    SubtitlesData mySubtitlesData;
    [SerializeField] TextMeshProUGUI tmproDialogueText;
    [SerializeField] TextMeshProUGUI tmproCharNameText;
    [SerializeField] Image charSpriteImage;
    [SerializeField] Image dialogueBGImage;


    //falcon=1, eagle=2, raven=3, hawk=4, alveriumsoldier=5
    [SerializeField] Sprite falconSpriteRef;
    [SerializeField] Sprite eagleSpriteRef;
    [SerializeField] Sprite ravenSpriteRef;
    [SerializeField] Sprite hawkSpriteRef;
    [SerializeField] Sprite blankSpriteRef;

    int currentDialogueLine = 0;
    bool isActive = false;

    void Start()
    {
        mySubtitlesData = this.GetComponent<SubtitlesData>();
        currentDialogueLine = 0;
        charSpriteImage.sprite = null;
        EndSubtitles();
    }

    private void Update()
    {

    }

    public void StartSubtitles()
    {
        //if subtitles enabled, isActive=True
        isActive = true;
        tmproCharNameText.enabled = true;
        charSpriteImage.enabled = true;
        if (GameManager.Instance.saveData.SettingsData.ToggleSubtitles)
        {
	        tmproDialogueText.enabled = true;
	        dialogueBGImage.enabled = true;
        }
        TriggerNextDialogueLine();
    }

    // Update is called once per frame
    public void TriggerNextDialogueLine()
    {
        if( !isActive ) return;


        if( currentDialogueLine >= mySubtitlesData.cutsceneSubtitlesArray.Length-1 )
        {
            EndSubtitles();
        } else
        {
            currentDialogueLine += 1;
            UpdateSprite();
            tmproDialogueText.text = mySubtitlesData.cutsceneSubtitlesArray[currentDialogueLine].dialogueLine;
        }
    }

    void EndSubtitles()
    {
        Debug.Log("NEED SUBTITLES");
        isActive = false;
        tmproDialogueText.enabled = false;
        tmproCharNameText.enabled = false;
        charSpriteImage.enabled = false;
        dialogueBGImage.enabled = false;
    }

    //falcon=1, eagle=2, raven=3, hawk=4, blank=5
    void UpdateSprite()
    {
        switch( mySubtitlesData.cutsceneSubtitlesArray[currentDialogueLine].charInt )
        {
            case 1:
                tmproCharNameText.text = "Falcon";
                charSpriteImage.sprite = falconSpriteRef;
                break;

            case 2:
                tmproCharNameText.text = "Eagle";
                charSpriteImage.sprite = eagleSpriteRef;
                break;

            case 3:
                tmproCharNameText.text = "Raven";
                charSpriteImage.sprite = ravenSpriteRef;
                break;
            
            case 4:
                tmproCharNameText.text = "Hawk";
                charSpriteImage.sprite = hawkSpriteRef;
                break;

            case 5:
                tmproCharNameText.text = null;
                charSpriteImage.sprite = blankSpriteRef;
                break;

            default:
                print("oopsie, couldn't update sprite");
                break;

        }
    }
}
