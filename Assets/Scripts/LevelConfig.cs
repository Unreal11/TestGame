using UnityEngine;

[CreateAssetMenu(fileName = "New LevelConfig", menuName = "Level Config Data", order = 2)]
public class LevelConfig : ScriptableObject
{
    public int[] Rows
    {
        get
        {
            return rows;
        }
    }

    public int[] Columns
    {
        get
        {
            return columns;
        }
    }

    [SerializeField] 
    private int[] rows;

    [SerializeField] 
    private int[] columns; 
}
