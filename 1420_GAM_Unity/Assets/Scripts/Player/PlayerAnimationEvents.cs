using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    Player player;
    public void Start()
    {
        player = PlayerManager.instance.player;
    }

    public void EndAttack()
    {
        player.stateMachine.Changestate(player.idle);
        Debug.Log("Attack ended");
    }



}
