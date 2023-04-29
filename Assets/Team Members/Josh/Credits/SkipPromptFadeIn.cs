using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkipPromptFadeIn : MonoBehaviour
{
    TextMeshProUGUI myText;
    [SerializeField] float fadeSpeed;

    private void Start()
    {
        myText = this.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(TextFadeIn());
    }

    public IEnumerator TextFadeIn()
    {
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0);
        while( myText.color.a < 1.0f )
        {
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, myText.color.a + (Time.deltaTime / fadeSpeed));
            yield return null;
        }
    }
}
