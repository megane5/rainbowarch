using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Stage : MonoBehaviour
{

    public GameObject soilPrefab_;
    public GameObject glassPrefab_;
    public GameObject greenPrefab_;
    public GameObject redPrefab_;
    public GameObject bluePrefab_;
    public GameObject torchPrefab_;
    public GameObject ivyPrefab_;
    public GameObject waterPrefab_;
    public GameObject fallPrefab_;
    public GameObject shatterPrefab_;
    public GameObject greenOrbPrefab_;
    public GameObject redOrbPrefab_;
    public GameObject blueOrbPrefab_;
    public GameObject backPrefab_;
    private GameObject[] objects_;
    private int[] numObjects_;
    private TextAsset csvFile_;
    private List<List<int>> csvDatas_ = new List<List<int>>();
    private float blockLength_ = 0.32f;
    private float backLength_ = 2.88f;
    private float backWidth_ = 2.56f;

    // 0:None 1:Soil 2:Glass

    void Start()
    {
        csvFile_ = Resources.Load("CSVFiles/stage1") as TextAsset;
        StringReader reader = new StringReader(csvFile_.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] datas = line.Split(','); // リストに入れる
            List<int> intDatas = new List<int>();
            foreach(string data in datas)
            {
                intDatas.Add(int.Parse(data));
            }
            csvDatas_.Add(intDatas);
        }
        objects_ = new GameObject[] {null,soilPrefab_,glassPrefab_,greenPrefab_,redPrefab_,bluePrefab_,torchPrefab_,ivyPrefab_,waterPrefab_,fallPrefab_,shatterPrefab_,greenOrbPrefab_,redOrbPrefab_,blueOrbPrefab_};
        numObjects_ = new int[256];
        for (int i = 0; i < 30; i++)
        {
            Instantiate(backPrefab_, new Vector3(i*2.56f+0.118f,0.147f,0), Quaternion.identity);
        }
        CreateStage();
    }

    void Update()
    {

    }
    

    // ステージ作成
    public void CreateStage()
    {
        var parent = this.transform;

        // 配置する座標を設定
        Vector3 placePosition = new Vector3(-2.39f, 1.28f, -0.1f);
        // 初期化する座標を設定
        Vector3 initPosition = placePosition;

        // 配置する回転角を設定
        Quaternion q = new Quaternion();
        q = Quaternion.identity;

        /*
        // ブロック全削除
        var clones = GameObject.FindGameObjectsWithTag("Block");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
        */
        // 配置
        for (int i = 0; i < csvDatas_.Count; i++)
        {
            placePosition.x = initPosition.x;
            for (int j = 0; j < csvDatas_[i].Count; j++)
            {
                int item = csvDatas_[i][j];
                if (item != 0)
                {
                    Vector3 index = new Vector3((objects_[item].GetComponent<SpriteRenderer>().bounds.size.x - blockLength_) / 2,(objects_[item].GetComponent<SpriteRenderer>().bounds.size.y - blockLength_) / 2);
                    // ブロックの複製
                    GameObject block = Instantiate(objects_[item], placePosition+index, Quaternion.identity);
                    // ステージのブロック番号で名前を生成
                    block.name = objects_[item].name;
                    if(block.name == "TORCH")
                    {
                        block.GetComponent<TorchManage>().number = numObjects_[item];
                    }
                    numObjects_[item]++;
                }
                placePosition.x += blockLength_;
            }
            placePosition.y -= blockLength_;
        }


    }
}