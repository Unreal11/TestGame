using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public Text MissionText
    {
        get
        {
            return missionText;
        }
    }

    public UnityEvent OnEndLevel
    {
        get
        {
            return onEndLevel;
        }
    }

    public List<GameObject> Blocks
    {
        set
        {
            blocks = value;
        }
        get
        {
            return blocks;
        }
    }
    [SerializeField]
    private UnityEvent onEndLevel;
    [SerializeField]
    private UnityEvent onEndGame;
    [SerializeField]
    private UnityEvent onStart;
    [SerializeField] 
    private int levelNum = 0;
    [SerializeField] 
    private int levelCurrent;
    [SerializeField] 
    private LevelConfig config;   
    [SerializeField] 
    private GameObject prefabBlock;
    [SerializeField] 
    private Text missionText;
    [SerializeField] 
    private List<BlockBundleData> blockTypes;
   
    private List<GameObject> blocks;
    private List<string> formerAnswer;

    public void Generate()
    {   
        if (levelCurrent == levelNum)
        {
            onEndGame.Invoke();

            return;
        }

        int columns = config.Columns[levelCurrent];
        int rows = config.Rows[levelCurrent];      
        int countToAdd = columns * rows - Blocks.Count;

        for (int i = 0; i < countToAdd; i++)
            Blocks.Add(Instantiate(prefabBlock, new Vector3(0f, 0f, 0f), Quaternion.identity));

        SetBlockPositions(columns,rows);
        SetBlockSprites(columns,rows);

        levelCurrent++;    
    }

    public void DeactivateBlocks()
    {
        foreach (GameObject cell in Blocks)
            cell.SetActive(false);
    }


    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Start()
    {
        formerAnswer = new List<string>();
        Blocks = new List<GameObject>();
        Generate();
        onStart.Invoke();
    }

    private void SetBlockPositions(int column, int row)
    {
        float xPos, yPos;

        xPos = 1 - column;
        yPos = row - 1;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Blocks[i * column + j].SetActive(true);
                Blocks[i * column + j].transform.position = new Vector3(xPos, yPos, 0f);
                Blocks[i * column + j].GetComponent<Block>().MainPosition = new Vector3(xPos, yPos, 0f);

                xPos += 2f;
            }

            xPos = 1 - column;
            yPos -= 2f;
        }

        for (int i = row * column; i < Blocks.Count - 1; i++)
            Blocks[i].SetActive(false);
    }

    private void SetBlockSprites(int column, int row)
    {
        Block block;
        int levelType = Random.Range(0, blockTypes.Count);
        int randomCard;
        int correctCardIndex = Random.Range(0, column * row);
        List<BlockData> dataBlocks = new List<BlockData>(blockTypes[levelType].BlockData);

        for (int i = 0; i < column * row; i++)
        {
            block = Blocks[i].GetComponent<Block>();
            randomCard = Random.Range(0, dataBlocks.Count);
            block.generator = this;

            block.Initialize(dataBlocks[randomCard]);
            dataBlocks.RemoveAt(randomCard);
        }

        block = Blocks[correctCardIndex].GetComponent<Block>();

        while (formerAnswer.Contains(block.Id))
            block.Initialize(dataBlocks[Random.Range(0, dataBlocks.Count)]);

        block.isCorrect = true;
        MissionText.text = "Find " + block.Id;

        formerAnswer.Add(block.Id);
        dataBlocks.Clear();
    }
}
