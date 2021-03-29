using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMenu : MonoBehaviour
{
    public GameObject[] options;
    public GameObject selectedMark;

    public RPGMenu prevMenu;

    protected int selectedOption;

    // Start is called before the first frame update
    void Start()
    {
        selectedOption = 0;
        
        PlaceMark();
    }

    public int OptionIndex()
    {
        return selectedOption;
    }

    public int MoveSelection(int offset)
    {
        selectedOption += offset;
        if (selectedOption < 0)
        {
            selectedOption = options.Length - 1;
        }
        if (selectedOption >= options.Length)
        {
            selectedOption = 0;
        }
        PlaceMark();
        return selectedOption;
    }

    public virtual ActionCode SelectOption()
    {
        return new ActionCode(ActionCode.Action.Null);
    }

    void PlaceMark()
    {
        RectTransform optionTransform = options[selectedOption].GetComponent<RectTransform>();
        RectTransform markTransform = selectedMark.GetComponent<RectTransform>();

        float local_x_pos = optionTransform.localPosition.x - optionTransform.rect.width / 2 - markTransform.rect.width / 2;
        markTransform.localPosition = new Vector2(local_x_pos, optionTransform.localPosition.y);

    }

    private void OnDisable()
    {
        //Makes sure selected option is always 0 on reload
        MoveSelection(-selectedOption);
    }
}


struct action
{

}