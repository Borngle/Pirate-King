using UnityEngine;

public class ArcherAnimator : MonoBehaviour {
    public Animator[] leftArchers;
    public Animator[] rightArchers;

    public void SetArcherAnimation(string animation, bool value, bool facingLeft) {
        Animator[] active;
        Animator[] inactive;
        if(facingLeft) {
            active = leftArchers;
            inactive = rightArchers;
        }
        else {
            active = rightArchers;
            inactive = leftArchers;
        }
        for(int i = 0; i < active.Length; i++) {
            active[i].SetBool(animation, value);
        }
        for(int i = 0; i < inactive.Length; i++) {
            inactive[i].SetBool(animation, false);
        }
    }

    public void ClearArcherAnimation(string animation) {
        for(int i = 0; i < leftArchers.Length; i++) {
            leftArchers[i].SetBool(animation, false);
        }
        for(int i = 0; i < rightArchers.Length; i++) {
            rightArchers[i].SetBool(animation, false);
        }
    }

}
