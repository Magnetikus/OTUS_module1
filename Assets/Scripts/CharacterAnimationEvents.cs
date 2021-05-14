using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;
    public Animation shootAnimation;
    public FootRun footRun;

    void Start()
    {
        character = GetComponentInParent<Character>();
        
    }

    void ShootEnd()
    {
        character.SetState(Character.State.Idle);
    }

    void AttackEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
        footRun.SoundStart();
    }

    void PunchEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
        footRun.SoundStart();
    }

    void DoDamage()
    {
        if (shootAnimation) shootAnimation.Play();
        Character targetCharacter = character.target.GetComponent<Character>();
        targetCharacter.DoDamage();
    }

    
}
