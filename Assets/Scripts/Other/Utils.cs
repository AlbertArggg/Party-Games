using System.Collections;
using UnityEngine;

public static class Utils
{
    public static IEnumerator PlayAnimAndSetStateWhenFinished(
        GameObject parent, 
        Animator animator, 
        string animName,
        bool activeStateAtTheEnd = false
    ) {
        animator.Play(animName);
        var animLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);
        parent.SetActive(activeStateAtTheEnd);
    }
}