
using UnityEngine;

public class StackUIManager : MonoBehaviour
{
    public void PushButtonClicked()
    {
        if (StackController.ActiveStack != null)
        {
            StackController.ActiveStack.Push();
        }
        else
        {
            Debug.LogWarning("Place the Stack on a surface first!");
        }
    }

    public void PopButtonClicked()
    {
        if (StackController.ActiveStack != null)
        {
            StackController.ActiveStack.Pop();
        }
        else
        {
            Debug.LogWarning("Place the Stack on a surface first!");
        }
    }
}
