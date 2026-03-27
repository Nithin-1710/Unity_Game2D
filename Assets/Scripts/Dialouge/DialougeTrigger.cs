using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}
