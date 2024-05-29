using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Elemets : MonoBehaviour
{
    [SerializeField] private Animator uiImageAnimator;
    private bool startTransitionText = false;
    public bool StartTransitionText { get { return startTransitionText; }  set { startTransitionText = value; } }
     private bool endTransitionText = false;
    public bool EndTransitionText { get { return endTransitionText; }  set { endTransitionText = value; } }

    [SerializeField] private TextMeshProUGUI transitionText; 
    [SerializeField] private Slider progres;
    
    string textWordWrite;
    private void Awake() 
    {
        uiImageAnimator = GetComponent<Animator>(); 
    
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetAnimatorBool(string animationName,bool value)
    {
        uiImageAnimator.SetBool(animationName,value);
    }
    public void ResetTriggerMethod(string resetAnimationName)
    {
        uiImageAnimator.ResetTrigger(resetAnimationName);
    }
    public IEnumerator StartAnimationTransitionText(string text)
    {

        textWordWrite = text + "    " + Scene_Manager.Instance.GetNextSceneName();

        for (int i = 0; i < textWordWrite.Length; i++)
        {
            yield return new WaitForSeconds(.1f);
            transitionText.text += textWordWrite[i]; 
            
            if(i == textWordWrite.Length - 1)
            {
                i= textWordWrite.Length;
                UIManager.Instance.IsProgresStart = true;
                break;
            }
        }
    }
    public IEnumerator EndAnimationTransitionText()
    {
        for (int j = textWordWrite.Length-1; j >= 0; j--)
        {
            yield return new WaitForSeconds(.1f);
            if(transitionText.text.Contains(textWordWrite[j]))
            {
                
                transitionText.text = transitionText.text.Remove(transitionText.text.IndexOf(textWordWrite[j]),1);
            }
            
            if(j == 0)
            {
                j= 0;
                UIManager.Instance.StageTransitionAnimationEnds = true;
                break;
                
            }
        }
    }
    public void ProgresInstallation(float addedValue)
    {

       
        if(progres.value < 1f)
        {
            progres.gameObject.SetActive(true);
            progres.value += addedValue * Time.deltaTime;
        }
        else if(progres.value >= 1f)
        {
            progres.value = 0f;
            UIManager.Instance.TransitionTextAnimationStarts = true;
            UIManager.Instance.IsProgresStart = false;
            progres.gameObject.SetActive(false);

        }
    }
}
