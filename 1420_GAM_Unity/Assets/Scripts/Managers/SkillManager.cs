using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public ManHoleSkill manholeSkill;
    public DashSkill dashSkill; 
    public LaunchSkill launchSkill;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        manholeSkill = GetComponent<ManHoleSkill>();
        dashSkill = GetComponent<DashSkill>();
        launchSkill = GetComponent<LaunchSkill>();
    }
}
