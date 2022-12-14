using UnityEngine;

public class Boss_Rage : StateMachineBehaviour
{
	[SerializeField] private AudioClip rageSound;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<BossHealth>().isInvulnerable = true;
		SoundManager.instance.PlaySound(rageSound);
	}


	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<BossHealth>().isInvulnerable = false;
	}
}