using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public ManHoleSkill skill1;
    public DashSkill dashSkill;   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        skill1 = GetComponent<ManHoleSkill>();
        dashSkill = GetComponent<DashSkill>();
    }
}
