using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Response
{
    public abstract class ApiBaseResponse
    {
     public bool Success { get; set; }
        protected ApiBaseResponse(bool success) => Success = success;   
        public TResultType GetOkResult<TResultType>()
        {
            if (this is ApiOkResponse<TResultType> apiOkResponse)
            {
                return apiOkResponse.Result;
            }
            throw new InvalidOperationException($"Response type {this.GetType().Name} is not ApiOkResponse");
        }

    }
    public sealed class ApiOkResponse<TResult> : ApiBaseResponse
    {
        public TResult Result { get; set; }
        public ApiOkResponse(TResult result) : base(true)
        {
            Result= result;
        }
    }
    public abstract class ApiNotFoundResponse : ApiBaseResponse
    {
        public string Message { get; set; }
        public ApiNotFoundResponse(string message) : base(false)
        {
            Message= message;
        }
    }

    public class TournamentNotFoundResponse : ApiNotFoundResponse
    {
        public TournamentNotFoundResponse(int id) : base($"Tournament with id {id} was not found")
        {
        }
    }
    public class GameNotFoundResponse : ApiNotFoundResponse
    {
        public GameNotFoundResponse(int id) : base($"Game with id {id} was not found")
        {
        }
    }
    public class BadRequestResponse : ApiBaseResponse
    {
        public string Message { get; set; }
        public BadRequestResponse(string message) : base(false)
        {
            Message= message;
        }
    
    }
}
