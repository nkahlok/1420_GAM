using UnityEngine;

public class Menu : MonoBehaviour
{
    /*//whether listening for user input
    private bool _isListening;

    //UI document, assigned in Inspector
    public UIDocument doc;

    //fade transition script, assigned in Inspector
    public FadeUI fade;

    private void Awake()
    {
        //stop listening for input
        _isListening = false;
        //register interaction events
        RegisterEvents();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fade.Reveal();
        //delay user input for transition
        StartCoroutine(EnableInputWithDelay(fade.duration));
    }

    private IEnumerator EnableInputWithDelay(float theDelay)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterEvent()
    {
        //retrieve UI document root
        VisualElement root = doc.rootVisualElement;

        //retrieve UI elements
        //buttons
        List<VisualElement> btns = root.Query(className: "btnMenu").Tolist();

        //iterate through buttons
        foreach(VisualElement aBtn in btns)
        {
            //register interaction events
            aBtn.RegisterCallback<ClickEvent, VisualElement>(OnClickBtn, aBtn);
        }
    }

    private void OnClickBtn(ClickEvent theEvt, VisualElement theBtn)
    {
        //if listening for input
        if (_isListening)
        {
            _isListening=false;
        }
    }*/
}
