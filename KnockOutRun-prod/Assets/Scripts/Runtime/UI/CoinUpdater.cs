using Euphrates;
using System.Collections.Generic;
using UnityEngine;

public class CoinUpdater : MonoBehaviour
{
    [SerializeReference] IntSO _coin;
    [SerializeField] List<Floater> _floaters = new List<Floater>();

    int _indx = 0;

    private void OnEnable() => _coin.OnChange += ShowChange;
    private void OnDisable() => _coin.OnChange -= ShowChange;

    private void ShowChange(int change)
    {
        if (change < 1)
            return;

        Floater sel = _floaters[_indx];

        string str = change.ToString("+#;-#;0");
        sel.UpdateText(str);

        int xPos = Random.Range(200, Screen.width - 200);
        int yPos = Random.Range(500, Screen.height - 500);

        sel.transform.position = new Vector2(xPos, yPos);
        sel.Show(.5f);

        _indx = _indx == _floaters.Count - 1 ? 0 : _indx + 1;
    }
}
