using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    public GameObject interactPrompt;
    [SerializeField]private DialougeTrigger dialougeTrigger;
    private bool isTalking=false;

    public float interactionDistance = 2f;

    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= interactionDistance && !isTalking)
        {
            interactPrompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !isTalking)
            {
                isTalking=true;
                interactPrompt.SetActive(false);
                dialougeTrigger.TriggerDialogue();
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }
}