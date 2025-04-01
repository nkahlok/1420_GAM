using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MusicStarter : MonoBehaviour
{
    public int musicNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (musicNumber == 0)
        {
            MusicManager.instance.PlayMusic("None");
        }
        if (musicNumber == 1)
        {
            MusicManager.instance.PlayMusic("Normal");
        }
        if (musicNumber == 2) 
        {
            MusicManager.instance.PlayMusic("Boss");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
