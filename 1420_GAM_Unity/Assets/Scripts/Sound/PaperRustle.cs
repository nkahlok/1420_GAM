using UnityEngine;

public class PaperRustle : MonoBehaviour
{
    public void PlayPaperSfx()
    {
        SoundManager.PlaySfx(SoundType.PAPER);
    }
}
