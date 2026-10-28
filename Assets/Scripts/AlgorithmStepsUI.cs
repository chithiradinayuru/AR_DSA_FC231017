
using UnityEngine;
using TMPro;

public class AlgorithmStepUI : MonoBehaviour
{
    public TMP_Text algorithmTitleText;
    public TMP_Text algorithmStepsText;

    private string[] currentSteps;
    private int currentStep = -1;
    private string currentOperation = "";

    void Start()
    {
        ShowPushAlgorithm();
    }

    public void ShowPushAlgorithm()
    {
        currentOperation = "PUSH";
        algorithmTitleText.text = "PUSH Algorithm";

        currentSteps = new string[]
        {
            "Check whether the stack is full",
            "Create a new element",
            "Place the element at TOP",
            "Update the TOP pointer"
        };

        currentStep = -1;
        UpdateStepsDisplay();
    }

    public void ShowPopAlgorithm()
    {
        currentOperation = "POP";
        algorithmTitleText.text = "POP Algorithm";

        currentSteps = new string[]
        {
            "Check whether the stack is empty",
            "Identify the TOP element",
            "Remove the TOP element",
            "Update the TOP pointer"
        };

        currentStep = -1;
        UpdateStepsDisplay();
    }

    public void NextStep()
    {
        if (currentSteps == null)
            return;

        if (currentStep >= currentSteps.Length - 1)
            return;

        currentStep++;
        UpdateStepsDisplay();

        StackController stack = StackController.ActiveStack;

        if (stack == null)
            return;

        // Perform the 3D operation at step 3
        if (currentStep == 2)
        {
            if (currentOperation == "PUSH")
            {
                stack.Push();
            }
            else if (currentOperation == "POP")
            {
                stack.Pop();
            }
        }
    }

    private void UpdateStepsDisplay()
    {
        if (algorithmStepsText == null || currentSteps == null)
            return;

        string displayText = "";

        for (int i = 0; i < currentSteps.Length; i++)
        {
            string line = (i + 1) + ". " + currentSteps[i];

            if (i == currentStep)
            {
                displayText +=
                    "<color=#00FF88><b>" +
                    line +
                    "</b></color>\n";
            }
            else
            {
                displayText +=
                    "<color=#FFFFFF>" +
                    line +
                    "</color>\n";
            }
        }

        algorithmStepsText.text = displayText;
    }
}
