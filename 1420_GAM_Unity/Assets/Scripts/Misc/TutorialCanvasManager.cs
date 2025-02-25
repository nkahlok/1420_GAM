using UnityEngine;

public class TutorialCanvasManager : MonoBehaviour
{
    private int count;
    public GameObject[] tutorialBoxes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialBoxes[0].SetActive(true);

        for(int i = 1; i < tutorialBoxes.Length; i++)
        {
            tutorialBoxes[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && count < tutorialBoxes.Length)
        {
        

            tutorialBoxes[count].SetActive(false);
            count++;

            if (count >= tutorialBoxes.Length)
            {
                count = 0;
            }

            tutorialBoxes[count].SetActive(true);

           
        }

        

    }
}
