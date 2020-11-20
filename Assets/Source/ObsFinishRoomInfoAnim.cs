using UnityEngine;
using UnityEngine.UI;

public class ObsFinishRoomInfoAnim : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var button = GameObject.Find("RoomCreateButton");
        if (button != null)
        {
            var Create = button.GetComponent<RoomCreateButton>();
            if (Create.IsPushed)
            {
                PhotonNetwork.CreateRoom(Create.RoomName, new RoomOptions { MaxPlayers = (byte)Create.PlayerNumber }, new TypedLobby());
            }

        }

         GameObject.Find("NoTouchPanel").GetComponent<Image>().enabled = false;
         Destroy(GameObject.Find("RoomInfoBoard"));
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
