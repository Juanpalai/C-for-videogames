using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public List<LevelBlock> currentLevelBlock = new List<LevelBlock>();

    public Transform levelStarPosition;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInicalBlock();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBLock()
    {
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count);

        LevelBlock block;

        Vector3 spawnposition = Vector3.zero;

        //The first block that is always the same at the start of the game is instantiated and the spawn position is also instantiated.
        if (currentLevelBlock.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[0]);
            spawnposition = levelStarPosition.position;
        }
        //since the game is started, the random block generation is used
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnposition = currentLevelBlock[currentLevelBlock.Count - 1].endPoin.position;
        }

        block.transform.SetParent(this.transform,false);

        //The position of the start of the second level block is aligned with the end of the first level block
        Vector3 corrention = new Vector3(
            spawnposition.x - block.starPoin.position.x, 
            spawnposition.y - block.starPoin.position.y,
            0);
        block.transform.position = corrention;

        currentLevelBlock.Add(block);
    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlock[0];
        currentLevelBlock.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllLevelBlock()
    {
        while(currentLevelBlock.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    public void GenerateInicalBlock()
    {
        for(int i = 0; i < 2; i++)
        {
            AddLevelBLock();
        }
    }

}
