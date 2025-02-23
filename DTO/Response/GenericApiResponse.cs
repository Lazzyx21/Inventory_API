namespace Inventory_API.DTO.Response
{
    public class GenericApiResponse<T>
    {
        /// <summary>
        /// Processing Status on API
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// Processing Result Message from API
        /// </summary>
        public string ErrorDesc { get; set; }
        /// <summary>
        /// Processing Result Message from API
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Data PayLoad if any has a result of processing
        /// </summary>
        public T Data { get; set; }
    }
}
