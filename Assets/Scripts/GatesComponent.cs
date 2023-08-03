using TestAssignment.Core;
using UnityEngine;

public class GatesComponent : MonoBehaviour
{
    [SerializeField]
    private Animation _animation;

    [SerializeField]
    private Collider _collider;

    private const string _idleAnimationName = "GatesIdleAnim";
    private const string _openAnimationName = "OpenDoorAnim";

    public System.Action GoToNextLevel;

    private void OnEnable()
    {
        _animation.Play(_idleAnimationName);
        _collider.enabled = false;

        GameManager.Instance.LevelCompleted += Open;
    }

    private void OnDisable()
    {
        GameManager.Instance.LevelCompleted -= Open;
        GoToNextLevel = null;
    }

    public void Open()
    {
        _animation.Play(_openAnimationName);
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Next Level");
        GoToNextLevel?.Invoke();
    }
}
