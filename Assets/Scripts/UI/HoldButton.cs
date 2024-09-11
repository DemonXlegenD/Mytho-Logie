using UnityEngine;
using UnityEngine.Events;

public class HoldButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnHoldDone;
    private Fill fillComp;
    [SerializeField] private float duration = 3f;
    private float currentTime = 0f;
    private bool isHolding = false;

    private void Start()
    {
        fillComp = GetComponent<Fill>();
    }
    public void OnHold()
    {
        isHolding = true;
    }

    public void UnHold()
    {
        isHolding = false;
    }

    private void Update()
    {
        if (isHolding)
        {
            currentTime = Mathf.Min(currentTime + Time.deltaTime, duration) ;
            if (currentTime == duration)
            {
                OnHoldDone.Invoke();
                isHolding = false;
            }
        }
        else
        {
            if (currentTime > 0 && currentTime != duration)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = 0f;
            }
        }
        fillComp.ChangeSize(currentTime / duration);
    }
}
