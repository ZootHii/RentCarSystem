using System.Text.Json;
using Newtonsoft.Json;

namespace Core.Utilities.Exceptions.Errors
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Type { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}