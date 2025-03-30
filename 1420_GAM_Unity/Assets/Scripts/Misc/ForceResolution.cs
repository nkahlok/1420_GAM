using UnityEngine;

public class ForceResolution : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        int screenW = 1920;
        int screenH = 1080;
        bool isFullScreen = false;

        Screen.SetResolution(screenW, screenH, isFullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
