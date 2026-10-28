
using UnityEngine;

public class StackUIManager : MonoBehaviour
{
    public AlgorithmStepUI algorithmStepUI;

    public void PushButtonClicked()
    {
        if (StackController.ActiveStack == null)
        {
            Debug.LogWarning("Place the Stack first!");
            return;
        }

        if (algorithmStepUI != null)
        {
            algorithmStepUI.ShowPushAlgorithm();
        }
    }

    public void PopButtonClicked()
    {
        if (StackController.ActiveStack == null)
        {
            Debug.LogWarning("Place the Stack first!");
            return;
        }

        if (algorithmStepUI != null)
        {
            algorithmStepUI.ShowPopAlgorithm();
        }
    }
}
