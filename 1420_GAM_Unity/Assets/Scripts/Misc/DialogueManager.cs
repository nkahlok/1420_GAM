using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialogueBoxes;
    public GameObject bossStarter;
    public GameObject boss;
    private int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossStarter.SetActive(false);

        for (int i = 1; i < dialogueBoxes.Length; i++)
        {
            dialogueBoxes[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && count < dialogueBoxes.Length)
        {
            dialogueBoxes[count].SetActive(false);
            count++;
            if(count < dialogueBoxes.Length)
                dialogueBoxes[count].SetActive(true);
        }

        if (count >= dialogueBoxes.Length)
        {
            bossStarter.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                boss.SetActive(true);
                bossStarter.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}
