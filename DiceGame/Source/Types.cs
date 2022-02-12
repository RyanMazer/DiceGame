using Newtonsoft.Json;

namespace DiceGame.Source
{
    public enum ELoadingState
    {
        S_Empty,
        S_Loading,
        S_Loaded,
        S_Uploading,
        S_Failed
    }

    public enum ESessionState
    {
        S_None,
        S_Open,
        S_Closed,
    }

    public enum EUploadType
    {
        T_Delete,
        T_Update
    }

    public class DiceJson
    {
        [JsonProperty("diceName")]
        public string diceName { get; set; }
        [JsonProperty("diceFace")]
        public string diceFace { get; set; }
    }
}
