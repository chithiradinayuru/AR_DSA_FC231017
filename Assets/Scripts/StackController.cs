
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    // Prefab used to create stack blocks
    public GameObject blockPrefab;

    // Stores the blocks in LIFO order
    private Stack<GameObject> blocks = new Stack<GameObject>();

    // Value assigned to the next block
    private int nextValue = 1;

    // Maximum number of blocks
    private const int maxBlocks = 5;

    // Reference to the Stack placed in AR
    public static StackController ActiveStack;

    // Runs automatically when the StackModel is created
    void Awake()
    {
        ActiveStack = this;
        Debug.Log("StackController initialized!");
    }

    // PUSH: Add a block to the top of the stack
    public void Push()
    {
        if (blocks.Count >= maxBlocks)
        {
            Debug.LogWarning("Stack Overflow: Stack is full!");
            return;
        }

        if (blockPrefab == null)
        {
            Debug.LogError("Block Prefab is not assigned!");
            return;
        }

        GameObject newBlock = Instantiate(
            blockPrefab,
            transform
        );

        float yPosition = 0.065f + blocks.Count * 0.09f;

        newBlock.transform.localPosition =
            new Vector3(0, yPosition, 0);

        newBlock.transform.localRotation =
            Quaternion.identity;

        newBlock.transform.localScale =
            blockPrefab.transform.localScale;

        newBlock.SetActive(true);

        newBlock.name = "Stack Block " + nextValue;

        blocks.Push(newBlock);

        Debug.Log("PUSH: Added " + nextValue);

        nextValue++;
    }

    // POP: Remove the top block from the stack
    public void Pop()
    {
        if (blocks.Count == 0)
        {
            Debug.LogWarning("Stack Underflow: Stack is empty!");
            return;
        }

        GameObject removedBlock = blocks.Pop();

        Debug.Log("POP: Removed " + removedBlock.name);

        Destroy(removedBlock);
    }

    // Clears the active reference when the stack is destroyed
    void OnDestroy()
    {
        if (ActiveStack == this)
        {
            ActiveStack = null;
        }
    }
}
