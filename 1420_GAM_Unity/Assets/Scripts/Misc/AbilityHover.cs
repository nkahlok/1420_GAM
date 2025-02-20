using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject toolkit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toolkit = this.gameObject.transform.GetChild(0).gameObject;
        toolkit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
        

    public void OnPointerEnter(PointerEventData eventData)
    {

        toolkit.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        toolkit.SetActive(false);
    }


}
