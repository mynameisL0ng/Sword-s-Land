using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;
    public static DataPersistenceManager instance {  get; private set; }
    private void Awake()
    {
        if (instance != null)
            Debug.Log("Found more than one data persistence manager in game scene");
        instance = this;
    }
    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();
        if(this.gameData == null)
        {
            Debug.Log("No data was found, Initializing data to default");
            NewGame();
        }
        foreach(IDataPersistence persistence in dataPersistenceObjects)
        {
            persistence.LoadData(gameData);
            Debug.Log("Load data from object");
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistence persistence in dataPersistenceObjects)
        {
            persistence.SaveData(ref gameData);
            Debug.Log("Save data from objects");
        }
        fileDataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
