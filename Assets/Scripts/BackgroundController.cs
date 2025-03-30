using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    #region Inspector
    [Header("References"), Space(6)]
    [SerializeField] private BubbleScore bubbleScore;
    [Space(4)]
    [SerializeField] private MaterialInstanceController instanceController;
    #endregion

    #region Custom Functions
    public void UpdateBackground()
    {
        float t = (bubbleScore.currentScore - bubbleScore.startTransitionScore) / (bubbleScore.endTransitionScore - bubbleScore.startTransitionScore);

        instanceController.materialInstance.SetFloat("_TransitionController", Mathf.Lerp(1, -1, t));
    }
    #endregion
}