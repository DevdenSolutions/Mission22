using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public class SaveDataLocalStorage : MonoBehaviour
{
    #region singleton

    private static SaveDataLocalStorage _instance;

    public static SaveDataLocalStorage Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    // private readonly string DataPath = "D:\\Project Support Files\\SumeruVuforia\\AppData\\";
    string DataPath;
    private readonly string FileName = "\\SoldierData.bin";
    private readonly string ETagFileName = "\\Etag.bin";
    [SerializeField]
    public DataToSave _dataToSave;
    [SerializeField]
    public DataToSave _dataToRead;
    private void Start()
    {
       DataPath =  Application.persistentDataPath;
    }
    public SoldierData FillData(MyRoot data, Texture2D texture)
    {
        SoldierData soldierData = new SoldierData();
        soldierData.Name = data.Name;
        soldierData.Hometown = data.Hometown;
        soldierData.DOB = data.DOB;
        soldierData.DOD = data.DOD;
        soldierData.Branch = data.Branch;
        soldierData.Designation = data.Designation;
        soldierData.ShortBio = data.ShortBio;
        soldierData.ImageWidth = texture.width;
        soldierData.ImageHeight = texture.height;
        soldierData.ImageToDetect = texture.EncodeToPNG();
        _dataToSave.soldierDatas.Add(soldierData);
        return soldierData;
    }

    public void WriteData(string Etag)
    {
        _dataToSave.ETag = Etag;
        SaveClassToLocalFile();
    }
    public void SaveClassToLocalFile()
    {
        FileStream fs = new FileStream(DataPath + FileName, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fs, _dataToSave);
        fs.Close();
    }

    public async Task<DataToSave> RetrieveDataFromLocal()
    {
        FileStream fs = new FileStream(DataPath + FileName, FileMode.Open, FileAccess.Read);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        await Task.Run(() =>
        {
            _dataToRead = binaryFormatter.Deserialize(fs) as DataToSave;
        });

        fs.Close();
        return _dataToRead;
    }

    public async Task<string> RetrieveETag()
    {
        string s = "";
        FileStream fs = new FileStream(DataPath + ETagFileName, FileMode.Open, FileAccess.Read);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        await Task.Run(() =>
        {
            s = binaryFormatter.Deserialize(fs) as string;
        });

        fs.Close();
        return s;
    }

    public void WriteETag(string ETag)
    {
        FileStream fs = new FileStream(DataPath + ETagFileName, FileMode.Create, FileAccess.Write);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fs, ETag);
        fs.Close();
    }

    public async void WriteAndRetrieveDataFromLocal(string Etag)
    {
        WriteData(Etag);
        //RetrieveDataFromLocal(); // Need to call after WriteData completes. For now its working. But if some problem comes it needs to wait until WriteData() is finished.
        Task<DataToSave> AsyncRetrieveDataFunction = SaveDataLocalStorage.Instance.RetrieveDataFromLocal();
        DataToSave _dataToRead = await AsyncRetrieveDataFunction;
        SaveDataLocalStorage.Instance.CreateSoldiersAfterRetrievingData(_dataToRead);
    }


    public void CreateSoldiersAfterRetrievingData(DataToSave dataToCreateSoldier)
    {

        foreach (var x in dataToCreateSoldier.soldierDatas)
        {
            Texture2D texture = new Texture2D(x.ImageWidth, x.ImageHeight);
            texture.LoadImage(x.ImageToDetect);
            SoldierDataManager.Instance.CreateSoldier(x.Name, x.TrackingImageURL, x.Hometown, x.DOB, x.DOD, x.Designation, x.Branch, x.ShortBio, texture);
        }
    }

    [System.Serializable]
    public class DataToSave
    {
        public string ETag;
        public List<SoldierData> soldierDatas;
    }
    [System.Serializable]
    public class SoldierData
    {
        public string Name;
        public string TrackingImageURL;
        public string Hometown;
        public string DOB;
        public string DOD;
        public string Designation;
        public string Branch;
        public string ShortBio;
        public byte[] ImageToDetect;
        public int ImageWidth;
        public int ImageHeight;
    }
}
