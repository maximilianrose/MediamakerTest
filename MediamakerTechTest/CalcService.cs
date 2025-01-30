using Azure;

namespace MediamakerTechTest
{
    public class CalcService : ICalcService
    {

        public UserCalcResponse GetUserCalcResponse(UserCalcRequest request)
        {

            var response = new UserCalcResponse();
            response.answer = 0;

            switch (request.Operation.ToLower())
            {

                case "add":
                    response.answer = request.FirstNumber + request.SecondNumber;
                    break;
                case "subtract":
                    response.answer = request.FirstNumber - request.SecondNumber;
                    break;
                case "multiply":
                    response.answer = request.FirstNumber * request.SecondNumber;
                    break;
                case "divide":
                    response.answer = request.FirstNumber/request.SecondNumber;
                    break;
                default:
                    response.answer = 0;
                    break;

            }

            return response;

        }


    }
}
