using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateFullCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    [SerializeField] private GameObject target;
    [SerializeField] private float speed = 200.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int layerMask = 1 << 8;

    private InputAction click;
    bool startedDrag;


    private void OnEnable()
    {
        click = InputSystem.actions.FindAction("Click");
        click.Enable();
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    Swipe();

        if (this.transform.rotation != target.transform.rotation) {
            var step = speed * Time.deltaTime;
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
        }
    }

    void Swipe()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && !GameManager.Instance.GetIsRotatingPiece() && !FindFirstObjectByType<Snail>().isLerping)
        {
            firstPressPos = Mouse.current.position.ReadValue();
            startedDrag = true;
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame && !GameManager.Instance.GetIsRotatingPiece() && !FindFirstObjectByType<Snail>().isLerping)
        {
            startedDrag = false;
            secondPressPos = Mouse.current.position.ReadValue();
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

           
            if (LeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
            else if (UpRightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownRightSwipe(currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);

            }

        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    }

    bool UpLeftSwipe(Vector2 swipe) {
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    }

    bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    }
}
