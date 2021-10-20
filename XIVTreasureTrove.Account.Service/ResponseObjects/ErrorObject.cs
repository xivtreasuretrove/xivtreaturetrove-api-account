namespace XIVTreasureTrove.Account.Service.ResponseObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorObject : MessageObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ErrorObject(string message) : base("")
        {
            ErrorMessage = message;
        }
    }
}
