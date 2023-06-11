using UnityEngine;

public class InstrumentController : MonoBehaviour
{
    [SerializeField] private SwipeManager _swipeManager;
    [SerializeField] private InstrumentLogic _instrumentLogic;

    private void OnEnable()
    {
        _swipeManager.OnSwipeDetected += OnSwipeDetected;
    }


    private void OnDisable()
    {
        _swipeManager.OnSwipeDetected -= OnSwipeDetected;
    }

    void OnSwipeDetected(Swipe direction)
    {
        switch (direction)
        {
            case Swipe.Up:
                _instrumentLogic.MoveToTop(); break;
            case Swipe.Down:
                _instrumentLogic.MoveToBottom(); break;
            case Swipe.Left:
                _instrumentLogic.MoveToLeft(); break;
            case Swipe.Right:
                _instrumentLogic.MoveToRight(); break;
        }
    }
}
