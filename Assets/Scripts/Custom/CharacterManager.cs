
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;

    private void Start()
    {
        SelectCharacter(0);
    }

    public void SelectCharacter(int index)
    {
        if (index < 0 || index >= characters.Length)
            return;

        selectedCharacter = index;

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null)
                characters[i].SetActive(i == index);
        }
    }

    public bool IsCharacterUnlocked(int index)
    {
        return index >= 0 && index < characters.Length;
    }
}
