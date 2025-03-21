using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldown;
    [HideInInspector] public float cooldownTimer;
    [HideInInspector] public Player player;


    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
        
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool SkillAvailable()
    {
        if (cooldownTimer < 0 && !player.isBusy)
        {
            CastSkill();
            cooldownTimer = cooldown;
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void CastSkill()
    {

    }
}
