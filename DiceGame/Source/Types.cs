using Newtonsoft.Json;

namespace DiceGame.Source
{
    /// <summary>
    /// Enum used to debug current state of dice list loading
    /// </summary>
    public enum ELoadingState
    {
        S_Empty,
        S_Loading,
        S_Loaded,
        S_Uploading,
        S_Failed
    }

    
    //public enum ESessionState
    //{
    //    S_None,
    //    S_Open,
    //    S_Closed
    //}

    /// <summary>
    /// Used to specify if given dicelist needs to be deleted or updated on the database
    /// </summary>
    public enum EUploadType
    {
        T_Delete,
        T_Update
    }

    /// <summary>
    /// Used to deserialize a Json body into a usable Dice object
    /// </summary>
    public class DiceJson
    {
        [JsonProperty("diceName")] public string DiceName { get; set; }

        [JsonProperty("diceFace")] public string DiceFace { get; set; }
    }

    /// <summary>
    /// Used to deserialize a Json body into a usable Server Session object
    /// </summary>
    public class ServerJson
    {
        [JsonProperty("Name")] public string SessionName { get; set; }

        [JsonProperty("Ip")] public string SessionIp { get; set; }

        [JsonProperty("Password")] public string SessionPassword { get; set; }
    }
}