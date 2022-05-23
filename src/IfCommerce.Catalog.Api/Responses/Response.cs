using System.Collections.Generic;

namespace IfCommerce.Catalog.Api.Responses
{
    public class Response
    {
        public string Instance { get; set; }
        public string TraceId { get; set; }
        public List<ResponseError> Errors { get; set; }

        public Response(string instance, string traceId)
        {
            Instance = instance;
            TraceId = traceId;
            Errors = new List<ResponseError>();
        }

        public bool IsValid()
        {
            return Errors.Count == 0;
        }
    }
}
