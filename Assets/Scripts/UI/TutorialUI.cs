using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialBox;
    public GameObject helpButton;

    public GameObject[] pages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTutorial()
    {
        tutorialBox.SetActive(true);
        helpButton.SetActive(false);
    }

    public void CloseTutorial()
    {
        tutorialBox.SetActive(false);
        helpButton.SetActive(true);
    }

    public void CyclePages()
    {
        if (TutorialManager.cycle == 0)
        {
            pages[0].SetActive(true);
            pages[1].SetActive(false);
            pages[2].SetActive(false);
            pages[3].SetActive(false);
            pages[4].SetActive(false);
            pages[5].SetActive(false);
            pages[6].SetActive(false);
        }
        else if (TutorialManager.cycle == 1)
        {
            pages[1].SetActive(true);
            pages[0].SetActive(false);
            pages[2].SetActive(false);
            pages[3].SetActive(false);
            pages[4].SetActive(false);
            pages[5].SetActive(false);
            pages[6].SetActive(false);
        }
        else if (TutorialManager.cycle == 2)
        {
            pages[2].SetActive(true);
            pages[0].SetActive(false);
            pages[1].SetActive(false);
            pages[3].SetActive(false);
            pages[4].SetActive(false);
            pages[5].SetActive(false);
            pages[6].SetActive(false);
        }
        else if (TutorialManager.cycle == 3)
        {
            pages[3].SetActive(true);
            pages[0].SetActive(false);
            pages[1].SetActive(false);
            pages[2].SetActive(false);
            pages[4].SetActive(false);
            pages[5].SetActive(false);
            pages[6].SetActive(false);
        }
        else if (TutorialManager.cycle == 4)
        {
            pages[4].SetActive(true);
            pages[0].SetActive(false);
            pages[1].SetActive(false);
            pages[2].SetActive(false);
            pages[3].SetActive(false);
            pages[5].SetActive(false);
            pages[6].SetActive(false);
        }
        else if (TutorialManager.cycle == 5)
        {
            pages[5].SetActive(true);
            pages[0].SetActive(false);
            pages[1].SetActive(false);
            pages[2].SetActive(false);
            pages[3].SetActive(false);
            pages[4].SetActive(false);
            pages[6].SetActive(false);
        }
        else if (TutorialManager.cycle == 6)
        {
            pages[6].SetActive(true);
            pages[0].SetActive(false);
            pages[1].SetActive(false);
            pages[2].SetActive(false);
            pages[3].SetActive(false);
            pages[4].SetActive(false);
            pages[5].SetActive(false);
        }

    }

    public void Next()
    {
        if (TutorialManager.cycle < 7 && TutorialManager.cycle != 6)
        {
            TutorialManager.cycle++;
            CyclePages();
        }
        Debug.Log(TutorialManager.cycle);
    }

    public void Back()
    {
        if (TutorialManager.cycle > -1 && TutorialManager.cycle < 7 && TutorialManager.cycle != 0)
        {
            TutorialManager.cycle--;
            CyclePages();
        }
        Debug.Log(TutorialManager.cycle);
    }
}
