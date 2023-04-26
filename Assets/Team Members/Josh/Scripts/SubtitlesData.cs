using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesData : MonoBehaviour
{
    
    [System.Serializable]
    public class SubtitleVars
    {
        //bool isLeft;
        [SerializeField] public string dialogueLine;
        [SerializeField] public int charInt;
    }

    public SubtitleVars[] cutsceneSubtitlesArray;
}
