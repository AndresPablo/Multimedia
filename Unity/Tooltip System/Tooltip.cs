using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Tooltips
{
[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement; 
    public int characterWrapLimit = 80;

    public RectTransform rectTransform;

    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();   
    }

    public void SetText(string content, string header = ""){
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header; 
        }

        contentField.text = content;
    }

    void Update()
    {
        if(Application.isEditor)
        {
             int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true: false;
        }


        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

}
}
