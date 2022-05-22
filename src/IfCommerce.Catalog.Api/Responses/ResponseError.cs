namespace IfCommerce.Catalog.Api.Responses
{
    public class ResponseError
    {
        public string Type { get; set; }
        public string Error { get; set; }
        public string Detail { get; set; }

        public ResponseError(string type, string error, string detail)
        {
            Type = type;
            Error = error;
            Detail = detail;
        }
    }
}
