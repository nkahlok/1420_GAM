using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float xOffset;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(PlayerManager.instance.player.transform.position.x + xOffset, PlayerManager.instance.player.transform.position.y + yOffset, this.transform.position.z);
    }
}
