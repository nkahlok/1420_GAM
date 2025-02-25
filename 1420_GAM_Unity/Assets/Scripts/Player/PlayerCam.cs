using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float xOffset;
    public float yOffset;

    public bool isBossLvl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBossLvl)
            this.transform.position = new Vector3(PlayerManager.instance.player.transform.position.x + xOffset, PlayerManager.instance.player.transform.position.y + yOffset, this.transform.position.z);
        else if (isBossLvl)
            this.transform.position = new Vector3(this.transform.position.x, PlayerManager.instance.player.transform.position.y + yOffset, this.transform.position.z);
    }
}
