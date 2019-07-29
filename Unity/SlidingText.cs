using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class SlidingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountLabel;
    [SerializeField] float lerpTime = 2f;
    [SerializeField] Color lerpingColor = Color.white;
    Color originalColor;
    float startingValue;
    float currentValue;
    float desiredValue;

    void Start()
    {
        originalColor = amountLabel.color;
    }

    public void SetVariable(float newAmount)
    {
        StartCoroutine("SlideNumber", newAmount);
    }

    IEnumerator SlideNumber(int newAmount)
    {
       startingValue = currentValue;

       float elapsedTime = 0;

       while(elapsedTime < lerpTime)
       {
            currentValue = Mathf.Lerp(startingValue, desiredValue, (float)(elapsedTime / lerpTime));
            amountLabel.text = currentValue.ToString();
            elapsedTime += Time.deltaTime;
            yield return null;
       }
        
    }


}
