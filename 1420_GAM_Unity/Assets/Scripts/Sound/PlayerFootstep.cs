using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.PlaySfx(SoundType.PLAYERWALK);
    }
}
