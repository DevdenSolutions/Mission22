// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
[Serializable]
public class Field
{
    public string rowId;
    public int sequence;
    public int isConfigure;
    public int favourite;
    public object totalItems;
    public object completedItems;
    public object dueDateTimestamp;
    public object checklistId;
    public object remainderId;
    public DateTime updatedAt;
    public DateTime createdAt;
    public string Name;
    public string TrackingURL;
    public string Hometown;
    public string DOB;
    public string DOD;
    public string Designation;
    public string Branch;
    public string ShortBio;
}
[Serializable]
public class Root
{
    public string id;
    public Field field;
}

