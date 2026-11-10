
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public CharacterManager manager;

    public void NextCharacter()
    {
        if (manager == null || manager.characters.Length == 0)
            return;

        int next = (manager.selectedCharacter + 1)
                   % manager.characters.Length;

        manager.SelectCharacter(next);
    }

    public void PreviousCharacter()
    {
        if (manager == null || manager.characters.Length == 0)
            return;

        int previous = (manager.selectedCharacter - 1
                        + manager.characters.Length)
                       % manager.characters.Length;

        manager.SelectCharacter(previous);
    }
}
