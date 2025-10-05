using UnityEngine;

public class GridSnap : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerControler playerController = animator.GetComponent<PlayerControler>();
        playerController.SnapPosition();
    }
}
