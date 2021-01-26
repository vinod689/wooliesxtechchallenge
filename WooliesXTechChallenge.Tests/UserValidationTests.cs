using Moq;
using System;
using System.Threading.Tasks;
using WooliesXTechChallenge.Application.Token;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models;
using Xunit;

namespace WooliesXTechChallenge.Tests
{
    public class UserValidationTests
    {
        #region Setup

        private readonly string _token = "2306c329-4b09-4bdf-b756-c2cfc207e744";
        private readonly ValidateUserHandler _validateUserHandler;


        public UserValidationTests()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(u => u.GenerateUserToken("Vinod Patwari"))
                .Returns(Task.FromResult(new TokenDetailsModel(_token, "Vinod Patwari")));
            _validateUserHandler = new ValidateUserHandler(userServiceMock.Object);
        }

        #endregion

        #region Test Cases

        [Fact(DisplayName = "Check the user is valid.")]
        public async Task ValidateUserAsync()
        {
            var result = await _validateUserHandler.Handle(new ValidateUserQuery(), new System.Threading.CancellationToken());
            Assert.Equal(_token, result.Token);
        }

        [Fact(DisplayName = "Check the length of name and token")]
        public async Task CheckNameAndTokenLength()
        {
            var expected = true;
            var result = await _validateUserHandler.Handle(new ValidateUserQuery(), new System.Threading.CancellationToken());
            Assert.Equal(expected, result.Name.Length > 0 && result.Token.Length > 0);
        }

        #endregion
    }
}
