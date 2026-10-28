
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [Header("Stack Settings")]
    public GameObject blockPrefab;
    public Transform topIndicator;

    private Stack<GameObject> blocks = new Stack<GameObject>();
    private Stack<int> values = new Stack<int>();

    private int nextValue = 1;
    private const int maxBlocks = 5;

    public static StackController ActiveStack;

    [Header("Animation Settings")]
    public float animationDuration = 0.5f;
    public float animationHeight = 0.25f;

    private bool isAnimating = false;

    void Awake()
    {
        ActiveStack = this;

        Debug.Log("StackController initialized!");
    }

    void Start()
    {
        UpdateTopIndicator();

        UpdateLearningPanel(
            "Stack placed. Press PUSH to add an element."
        );
    }

    // ---------------------------
    // PUSH OPERATION
    // ---------------------------

    public void Push()
    {
        if (isAnimating)
        {
            Debug.Log("Wait for the animation to finish.");
            return;
        }

        if (blocks.Count >= maxBlocks)
        {
            UpdateLearningPanel(
                "STACK OVERFLOW: The stack is full!"
            );
            return;
        }

        if (blockPrefab == null)
        {
            Debug.LogError("Block Prefab is not assigned!");
            return;
        }

        int value = nextValue;

        GameObject newBlock = Instantiate(
            blockPrefab,
            transform
        );

        float targetY = 0.065f + blocks.Count * 0.09f;

        Vector3 targetPosition =
            new Vector3(0, targetY, 0);

        Vector3 startPosition =
            targetPosition + Vector3.up * animationHeight;

        newBlock.transform.localPosition = startPosition;
        newBlock.transform.localRotation = Quaternion.identity;
        newBlock.transform.localScale =
            blockPrefab.transform.localScale;

        newBlock.SetActive(true);
        newBlock.name = "Stack Block " + value;

        blocks.Push(newBlock);
        values.Push(value);

        nextValue++;

        // Move TOP indicator to the new top block
        UpdateTopIndicator();

        UpdateLearningPanel(
            "PUSH(" + value + "): Adding " + value +
            " to the TOP of the stack."
        );

        StartCoroutine(
            AnimatePush(
                newBlock.transform,
                startPosition,
                targetPosition
            )
        );

        Debug.Log("PUSH: Added " + value);
    }

    // ---------------------------
    // PUSH ANIMATION
    // ---------------------------

    private IEnumerator AnimatePush(
        Transform block,
        Vector3 start,
        Vector3 target)
    {
        isAnimating = true;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / Mathf.Max(animationDuration, 0.01f)
            );

            if (block != null)
            {
                block.localPosition =
                    Vector3.Lerp(start, target, t);
            }

            yield return null;
        }

        if (block != null)
        {
            block.localPosition = target;
        }

        isAnimating = false;
    }

    // ---------------------------
    // POP OPERATION
    // ---------------------------

    public void Pop()
    {
        if (isAnimating)
        {
            Debug.Log("Wait for the animation to finish.");
            return;
        }

        if (blocks.Count == 0)
        {
            UpdateLearningPanel(
                "STACK UNDERFLOW: The stack is empty!"
            );
            return;
        }

        GameObject removedBlock = blocks.Pop();
        int removedValue = values.Pop();

        // Move TOP indicator to the next top block
        UpdateTopIndicator();

        UpdateLearningPanel(
            "POP(): Removing " + removedValue +
            " from the TOP. This demonstrates LIFO."
        );

        StartCoroutine(
            AnimatePop(removedBlock)
        );

        Debug.Log("POP: Removed " + removedValue);
    }

    // ---------------------------
    // POP ANIMATION
    // ---------------------------

    private IEnumerator AnimatePop(GameObject block)
    {
        isAnimating = true;

        Vector3 startPosition =
            block.transform.localPosition;

        Vector3 targetPosition =
            startPosition + Vector3.up * animationHeight;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / Mathf.Max(animationDuration, 0.01f)
            );

            if (block != null)
            {
                block.transform.localPosition =
                    Vector3.Lerp(
                        startPosition,
                        targetPosition,
                        t
                    );
            }

            yield return null;
        }

        if (block != null)
        {
            Destroy(block);
        }

        isAnimating = false;
    }

    // ---------------------------
    // TOP INDICATOR
    // ---------------------------

    private void UpdateTopIndicator()
    {
        if (topIndicator == null)
        {
            return;
        }

        // Hide the indicator when stack is empty
        if (blocks.Count == 0)
        {
            topIndicator.gameObject.SetActive(false);
            return;
        }

        topIndicator.gameObject.SetActive(true);

        float yPosition =
            0.065f + (blocks.Count - 1) * 0.09f;

        topIndicator.localPosition =
            new Vector3(-0.30f, yPosition, 0f);
    }

    // ---------------------------
    // LEARNING PANEL
    // ---------------------------

    private void UpdateLearningPanel(string explanation)
    {
        StackLearningUI learningUI =
            FindFirstObjectByType<StackLearningUI>();

        if (learningUI == null)
        {
            Debug.LogWarning(
                "StackLearningUI not found!"
            );
            return;
        }

        string topValue =
            values.Count > 0
                ? values.Peek().ToString()
                : "Empty";

        learningUI.UpdateDisplay(
            blocks.Count,
            maxBlocks,
            topValue,
            explanation
        );
    }

    // ---------------------------
    // CLEANUP
    // ---------------------------

    void OnDestroy()
    {
        if (ActiveStack == this)
        {
            ActiveStack = null;
        }
    }
}
