using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMenu : MonoBehaviour
{
    //reference to UI text elements that describe options in the menu
    public GameObject[] options;
    //reference to UI image element that signals currently selected option
    public GameObject selectedMark;

    //reference to previously loaded menu. Saved to know which menu to activate when closed.
    public RPGMenu prevMenu;

    protected int selectedOption;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        selectedOption = 0;
        
        PlaceMark();
    }

    public int OptionIndex()
    {
        return selectedOption;
    }
    

    /*Change selected option by specfied offset. By default selected options wrap around on both directions
     *offset:       ammount by which to change the selected option index. Sign indicates direction 
     */
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

    /*Obtain Action Code correspondent to currently selected option. To be defined by child classes
     *returns:      action code describing behaviour after submiting currently selected option
     */
    public virtual ActionCode SelectOption()
    {
        return new ActionCode(ActionCode.Action.Null);
    }

    /*Update selected mark position based on currently selected option
     * 
     */
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