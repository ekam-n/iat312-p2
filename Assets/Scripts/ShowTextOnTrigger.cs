using TMPro;
using UnityEngine;

public class ShowTextOnTrigger : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    private void Start()
    {
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1);
        //{
        //    messageText.gameObject.SetActive(true);
        //}
    }

}
