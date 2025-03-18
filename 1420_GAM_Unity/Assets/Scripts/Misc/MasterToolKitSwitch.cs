using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MasterToolKitSwitch : MonoBehaviour
{
    public GameObject[] toolkit;

    public Image briefcaseImageCD;

    public Text briefcaseTextCD;

    private bool isCooldown = false;

    private float cooldownTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        briefcaseTextCD.gameObject.SetActive(false);
        briefcaseImageCD.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTime = SkillManager.instance.manholeSkill.cooldownTimer;

        if (Input.GetKeyDown(KeyCode.E))
        {
            for(int i = 0; i < toolkit.Length; i++)
            {
                toolkit[i].SetActive(false);
            }
            
        }

        if (cooldownTime < 0)
        {
            isCooldown = false;
        }
        else
        {
            isCooldown = true;
        }

        if(isCooldown)
        {
            briefcaseTextCD.gameObject.SetActive(true);
            briefcaseTextCD.text = Mathf.RoundToInt(cooldownTime).ToString();
            briefcaseImageCD.fillAmount = cooldownTime/SkillManager.instance.manholeSkill.cooldown;

        }
        else
        {
            briefcaseTextCD.gameObject.SetActive(false);
            briefcaseImageCD.fillAmount = 0f;


        }

    }
}
