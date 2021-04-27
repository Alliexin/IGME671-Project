using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ObjectManager : MonoBehaviour
{

    private static ObjectManager instance;

    [Header("Menu")]
    public GameObject menu;

    [Header("Prefabs")]

    public GameObject normalBlockPrefab;
    public GameObject movingBlockPrefab;
    public GameObject foodPrefab;
    public GameObject coinPrefab;

    [Header("SpawnRates")]

    public float randomBlockSpawnRate;
    public float movingBlockSpawnRate;
    public float patternSpawnRate;

    public float coinSpawnRate;
    public float foodSpawnRate;

    public float ten_coin_rate;
    public float eight_coin_rate;
    public float six_coin_rate;
    public float three_coin_rate;

    [Header("Platform Disntance")]

    public float distance_min;
    public float distance_max;

    [Header("Path Block")]

    public int pathBlockCapacity;
    private ObjectContainer pathBlocks;

    [Header("Building Block")]

    public int buildingBlockCapacity;
    private ObjectContainer buildingBlocks;

    [Header("Moving Block")]

    public int movingBlockCapacity;
    private ObjectContainer movingBlocks;

    [Header("Food")]

    public Sprite[] food_fruit_sprites;
    public int foodCapacity;
    private ObjectContainer food;

    [Header("Coin")]

    public int coinCapacity;
    private ObjectContainer coins;

    public float distance_vertical;
    public float distance_horizontal;

    private float nextHeight = 2f;
    private int lastPattern;

    private Vector3 resetPosition = new Vector3(0, -5, 0);

    public static ObjectManager getInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {

        instance = this;

        EventSystem.current.OnPlayerGetsHigher += SpawnBlock;
        EventSystem.current.OnBounce += CheckToSpawn;

        lastPattern = -1;

        InitInstance();

        InitPosition();

        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            ResetGame();
            InitPosition();
        }
    }

    public void Begin()
    {
        menu.SetActive(false);
        ResetGame();
        InitPosition();
    }

    void CheckToSpawn()
    {
        if (nextHeight - PlayerMovement.getInstance().pos.y < 20f)
        {
            EventSystem.current.PlayerGetsHigher();
            EventSystem.current.PlayerGetsHigher();
        }
    }

    void InitInstance()
    {
        pathBlocks = new ObjectContainer(normalBlockPrefab, pathBlockCapacity);

        buildingBlocks = new ObjectContainer(normalBlockPrefab, buildingBlockCapacity);

        movingBlocks = new ObjectContainer(movingBlockPrefab, movingBlockCapacity);

        food = new ObjectContainer(foodPrefab, foodCapacity, 0);

        coins = new ObjectContainer(coinPrefab, coinCapacity, 5);
    }

    void InitPosition()
    {
        PlayerMovement.getInstance().gameObject.SetActive(true);
        PlayerMovement.getInstance().pos = new Vector3(0, -1, 0);
        PlayerMovement.getInstance().addForce(new Vector2(0, 2000));

        CameraMovement.current.MoveToOrigin();

        //Set up initial positions
        for (int i = 0; i < pathBlockCapacity; i++)
        {
            pathBlocks[i].SetActive(true);
            pathBlocks[i].transform.position = new Vector2(Random.Range(-2f, 2f), nextHeight);
            SpawnCoinsAndFood(pathBlocks[i]);
            SpawnRandomBlock();
            nextHeight += Random.Range(2f, 2.4f);
        }
    }

    void ResetGame()
    {
        nextHeight = 2f;

        PlayerMovement.getInstance().gameObject.SetActive(false);

        foreach(GameObject g in pathBlocks.container)
        {
            g.transform.position = resetPosition;
            g.SetActive(false);
        }
        foreach (GameObject g in buildingBlocks.container)
        {
            g.transform.position = resetPosition;
            g.SetActive(false);
        }
        foreach (GameObject g in movingBlocks.container)
        {
            g.transform.position = resetPosition;
            g.SetActive(false);
        }
        foreach (GameObject g in food.container)
        {
            g.transform.position = resetPosition;
            g.SetActive(false);
        }
        foreach (GameObject g in coins.container)
        {
            g.transform.position = resetPosition;
            g.SetActive(false);
        }
    }

    void UpdatePathBlock()
    {
        pathBlocks.Activate();
        pathBlocks.GetOldest().transform.position = new Vector2(Random.Range(-2f, 2f), nextHeight);

        SpawnCoinsAndFood(pathBlocks.GetOldest());

        SpawnRandomBlock();

        UpdateNextHeight();

        pathBlocks.Cycle();
    }

    void UpdatePathBlockMoving()
    {
        movingBlocks.Activate();
        movingBlocks.GetOldest().GetComponent<MovingPlatform>().SetHeight(nextHeight);

        UpdateNextHeight();

        movingBlocks.Cycle();
    }

    void SpawnPattern() //Spawn a pattern according the the pattern database 
    {
        int selected = Random.Range(0, PatternDataBase.current.patterns.Count);
        while (selected == lastPattern)
        {
            selected = Random.Range(0, PatternDataBase.current.patterns.Count);
        }

        if (PatternDataBase.current.patterns[selected].platforms.Count == 0)
            return;

        Debug.Log("Pattern " + PatternDataBase.current.patterns[selected].name);
        for (int i = 0; i < PatternDataBase.current.patterns[selected].platforms.Count; i++)
        {

            //Get that position
            Vector2 vector2 = new Vector2(PatternDataBase.current.patterns[selected].platforms[i].pos.x, PatternDataBase.current.patterns[selected].platforms[i].pos.y + nextHeight);

            switch (PatternDataBase.current.patterns[selected].platforms[i].type)
            {
                case Platform.PlatformType.normal:
                    //Move the block to that position
                    MoveBuildingBlock(vector2);
                    break;
                case Platform.PlatformType.moving:
                    float height = PatternDataBase.current.patterns[selected].platforms[i].pos.y + nextHeight;
                    MoveMovingBlock(height);
                    break;
                case Platform.PlatformType.sequential:
                    break;
                case Platform.PlatformType.dissolve:
                    break;
                case Platform.PlatformType.switch_a:
                    break;
                case Platform.PlatformType.switch_b:
                    break;
                default:
                    Debug.Log("Wrong type when spawning patterns." + " Pattern: " + selected + "Plat: " + i);
                    break;
            }

            float spawnHeight = distance_vertical;
            //spawn coin
            if (PatternDataBase.current.patterns[selected].platforms[i].coin > 0)
            {
                int num = PatternDataBase.current.patterns[selected].platforms[i].coin;
                for (int j = 0; j < num; j++)
                {
                    MoveOldest(coins, vector2 + new Vector2(0, spawnHeight));
                    spawnHeight += distance_vertical;
                }
            }

            if(PatternDataBase.current.patterns[selected].platforms[i].food > 0)
            {
                int num = PatternDataBase.current.patterns[selected].platforms[i].food;
                for (int j = 0; j < num; j++)
                {
                    MoveOldest(food, vector2 + new Vector2(0, spawnHeight));
                    spawnHeight += distance_vertical;
                }
            }
        }
        nextHeight += PatternDataBase.current.patterns[selected].height;
        UpdateNextHeight();
    }

    void SpawnBlock() //Spawn a normal/moving block 
    {
        float temp = Random.Range(0f, 1f);

        if (temp < patternSpawnRate)
        {
            SpawnPattern();
        }
        else if (temp < patternSpawnRate + movingBlockSpawnRate)
        {
            UpdatePathBlockMoving();
        }
        else
        {
            UpdatePathBlock();
        }
    }

    void UpdateNextHeight() //Increse Next height for future platform generations 
    {
        nextHeight += Random.Range(distance_min, distance_max);
    }

    void SpawnRandomBlock() //Spawn Rnadom Blocks according to random spawn rate 
    {
        if (randomBlockSpawnRate > 1f)
        {
            for (int i = 0; i < (int)randomBlockSpawnRate; i++)
            {
                buildingBlocks.Activate();
                buildingBlocks.GetOldest().transform.position = RandomBlockPos();
                SpawnCoinsAndFood(buildingBlocks.GetOldest());
                buildingBlocks.Cycle();
            }
        }

        if (Random.Range(0.0f, 1.0f) < randomBlockSpawnRate % 1)
        {
            buildingBlocks.Activate();
            buildingBlocks.GetOldest().transform.position = RandomBlockPos();
            SpawnCoinsAndFood(buildingBlocks.GetOldest());
            buildingBlocks.Cycle();
        }
    }

    void SpawnCoinsAndFood(GameObject target) //Spawn food on target platform
    {
        float selector = Random.Range(0, 1f);
        if (selector < coinSpawnRate)
        {
            selector = Random.Range(0f, 1f);
            Vector2 pos = target.transform.position;

            if (selector < ten_coin_rate)
            {
                for (int i = 1; i <= 5; i++)
                {
                    MoveOldest(coins, pos + new Vector2(-distance_horizontal, distance_vertical * i));
                }

                for (int i = 1; i <= 5; i++)
                {
                    MoveOldest(coins, pos + new Vector2(distance_horizontal, distance_vertical * i));
                }
            }
            else if (selector < eight_coin_rate)
            {
                for (int i = 1; i <= 3; i++)
                {
                    MoveOldest(coins, pos + new Vector2(-distance_horizontal, distance_vertical * i));
                    MoveOldest(coins, pos + new Vector2(distance_horizontal, distance_vertical * (i + 1)));
                }
            }
            else if (selector < six_coin_rate)
            {
                for (int i = 1; i <= 3; i++)
                {
                    MoveOldest(coins, pos + new Vector2(-distance_horizontal, distance_vertical * i));
                }

                for (int i = 1; i <= 3; i++)
                {
                    MoveOldest(coins, pos + new Vector2(distance_horizontal, distance_vertical * i));
                }
            }
            else if (selector < three_coin_rate)
            {
                for (int i = 1; i <= 3; i++)
                {
                    MoveOldest(coins, pos + new Vector2(0, distance_vertical * i));
                }
            }
            else
            {
                MoveOldest(coins, pos + new Vector2(0, distance_vertical));
            }
        }
        else if (selector < coinSpawnRate + foodSpawnRate)
        {
            selector = Random.Range(0f, 1f);
            Vector2 pos = target.transform.position;

            if(selector < .5f)
            {
                for (int i = 1; i <= 3; i++)
                {
                    MoveOldest(coins, pos + new Vector2(0, distance_vertical * i));
                    if(i == 3)
                    {
                        MoveOldest(food, pos + new Vector2(0, distance_vertical * (i + 1)));
                    }
                }
            }
            else
            {
                MoveOldest(food, pos + new Vector2(0, distance_vertical));
            }
        }
        else
        {
            return;
        }
    }

    void MoveOldest(ObjectContainer container, Vector2 pos)
    {
        container.Activate();
        container.GetOldest().transform.position = new Vector3(pos.x, pos.y, container.GetOldest().transform.position.z);
        container.Cycle();
    }

    #region Utility
    Vector2 RandomBlockPos() //Generate a position near the newest path block
    {
        Vector2 pos = new Vector2(Random.Range(-2f, 2f), nextHeight + Random.Range(-1.8f, 1.8f));
        int target = pathBlocks.oldest;

        if (target == 0)
        {
            target = pathBlockCapacity - 1;
        }
        else
        {
            target = pathBlocks.oldest - 1;
        }

        while (Vector2.Distance(pos, pathBlocks[target].transform.position) < 0.7f && Vector2.Distance(pos, buildingBlocks.GetOldest().transform.position) < 0.7f)
        {
            pos = new Vector2(Random.Range(-2f, 2f), nextHeight + Random.Range(-1.8f, 1.8f));
        }

        return pos;
    }

    void MoveBuildingBlock(Vector2 pos) //Move oldest building block to location 
    {
        buildingBlocks.Activate();
        buildingBlocks.GetOldest().transform.position = pos;
        buildingBlocks.Cycle();
    }

    void MoveMovingBlock(float height) //Move oldest moving block to height
    {
        movingBlocks.Activate();
        movingBlocks.GetOldest().GetComponent<MovingPlatform>().SetHeight(height);

        movingBlocks.Cycle();
    }

    #endregion

    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(0,nextHeight,0), 1);
    }
    #endregion


}
