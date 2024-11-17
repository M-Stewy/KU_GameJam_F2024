using UnityEngine;
using UnityEngine.UI;

public class HasThisLevelBeenBeaten : MonoBehaviour
{
    [SerializeField] string LevelName;
    Image check;
    private void Start()
    {
        check = GetComponent<Image>();
        check.enabled = false;

        if(GameManagment.Instance.CompletedStages.Contains(LevelName))
        {
            check.enabled = true;
        }
    }
}
