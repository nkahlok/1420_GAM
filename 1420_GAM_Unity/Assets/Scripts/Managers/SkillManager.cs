using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public Skill1 skill1;
    public DashSkill dashSkill;   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        skill1 = GetComponent<Skill1>();
        dashSkill = GetComponent<DashSkill>();
    }
}
