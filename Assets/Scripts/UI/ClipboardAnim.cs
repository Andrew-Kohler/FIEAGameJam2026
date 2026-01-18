using UnityEngine;

public class ClipboardAnim : MonoBehaviour
{
    public GameObject clipboard;
    public Animator boardAnim;

    public bool isOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clipboard()
    {
        boardAnim.GetComponent<Animator>();

        if (isOpen == false)
        {
            boardAnim.SetTrigger("isOpen");
            isOpen = true;
        }

        else if (isOpen == true)
        {
            boardAnim.SetTrigger("isClose");
            isOpen = false;
        }
    }
}
