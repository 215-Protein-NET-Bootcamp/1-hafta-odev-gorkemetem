namespace ProteinInterestApi.Controllers

{
     public class CommonResponse<T>
     {

        public CommonResponse(T data)
        {
            Data = data;
        }
            

        public bool Success { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }

     }
        
}
