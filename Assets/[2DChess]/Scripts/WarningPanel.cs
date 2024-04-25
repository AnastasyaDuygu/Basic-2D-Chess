using System.Collections;
using UnityEngine;

public class WarningPanel : MonoBehaviour
{
    [SerializeField] private Animator warningAnimation;
    public void EnableWarningCanvas()
    {
        gameObject.SetActive(true);
        StartCoroutine(WaitTillAnimEnds());
    }
    IEnumerator WaitTillAnimEnds()
    {
        yield return new WaitForSeconds((warningAnimation.GetCurrentAnimatorStateInfo(0).length + warningAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime));
        gameObject.SetActive(false);
    }
}
