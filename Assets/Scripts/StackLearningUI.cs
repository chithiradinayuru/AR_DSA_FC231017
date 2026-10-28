
using UnityEngine;
using TMPro;

public class StackLearningUI : MonoBehaviour
{
    public TMP_Text stackSizeText;
    public TMP_Text topElementText;
    public TMP_Text operationText;

    public void UpdateDisplay(
        int currentSize,
        int maxSize,
        string topValue,
        string explanation)
    {
        if (stackSizeText != null)
        {
            stackSizeText.text =
                "Stack Size: " + currentSize + " / " + maxSize;
        }

        if (topElementText != null)
        {
            topElementText.text = "TOP: " + topValue;
        }

        if (operationText != null)
        {
            operationText.text = explanation;
        }
    }
}
