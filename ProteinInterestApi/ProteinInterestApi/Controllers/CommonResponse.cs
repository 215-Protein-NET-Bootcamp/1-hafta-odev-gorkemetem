namespace ProteinInterestApi.Controllers

{
     public class CommonResponse<T>
     {

        public CommonResponse(T data)
        {
            Data = data;
            Success = true;
        }

        public CommonResponse(string error)
        {
            Error = error;
            Success = false;
        }


        public bool Success { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }

     }
        
}
