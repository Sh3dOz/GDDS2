using UnityEngine;
using UnityEngine.UI;

public class DropDownColor : MonoBehaviour {
    public Dropdown dropdown;
    public Image spriteDisplay;

    public Sprite[] sprites; // Array of sprites for each option

    private void Start() {
        // Clear existing options from the dropdown
        dropdown.ClearOptions();

        // Add options to the dropdown based on the specified texts
        int optionCount = Mathf.Min(sprites.Length, 3);
        for (int i = 0; i < optionCount; i++) {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = GetDropdownText(i);
            dropdown.options.Add(option);
        }

        // Set the initial sprite
        UpdateSprite(LevelSelect.index);
        dropdown.value = LevelSelect.index;

        // Register a callback for when the dropdown value changes
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index) {
        // Update the sprite when the dropdown value changes
        UpdateSprite(index);
    }

    private void UpdateSprite(int index) {
        // Set the sprite display to the selected sprite
        spriteDisplay.sprite = sprites[index];
    }

    private string GetDropdownText(int index) {
        // Specify the desired dropdown texts here
        string[] dropdownTexts = { "Easy", "Normal", "Hard" };

        // Return the text corresponding to the given index
        if (index >= 0 && index < dropdownTexts.Length) {
            return dropdownTexts[index];
        }

        // Return an empty string if the index is out of range
        return "";
    }
}
