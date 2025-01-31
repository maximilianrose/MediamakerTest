using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MediamakerTechTest.Controllers;
using MediamakerTechTest;

namespace MediamakerUnitTest
{
    public class UnitTest1
    {
        private readonly Mock<ICalcService> _calcService;
        private readonly ValuesController _controller;

        public UnitTest1()
        {
            _calcService = new Mock<ICalcService>();
            _controller = new ValuesController(_calcService.Object);
        }

        [Fact]
        public void Test1()
        {
            var request = new UserCalcRequest { Operation = "multiply", FirstNumber = 10, SecondNumber = 7 };
            var expectedResponse = new UserCalcResponse { answer = 70 };

            _calcService
                .Setup(service => service.GetUserCalcResponse(request))
                .Returns(expectedResponse);

            var result = _controller.CalculateSum(request) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<UserCalcResponse>(result.Value);
            Assert.Equal(70, ((UserCalcResponse)result.Value).answer);
        }


        [Fact]
        public void BadRequestTest()
        {

            var invalidRequest = new UserCalcRequest { Operation = "add", FirstNumber = int.Parse("9.3"), SecondNumber = 3 };


            var result = _controller.CalculateSum(invalidRequest) as BadRequestObjectResult;


            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("Invalid request", result.Value.ToString());
        }


    }
}