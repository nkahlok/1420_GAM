using UnityEngine;

public class MasterToolKitSwitch : MonoBehaviour
{
    public GameObject[] toolkit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for(int i = 0; i < toolkit.Length; i++)
            {
                toolkit[i].SetActive(false);
            }
            
        }
    }
}
