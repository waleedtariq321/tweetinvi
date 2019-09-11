﻿using System.Threading.Tasks;
using FakeItEasy;
using Tweetinvi.Controllers.User;
using Tweetinvi.Core.Factories;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Models.DTO.QueryDTO;
using Tweetinvi.Parameters;
using Xunit;
using xUnitinvi.TestHelpers;

namespace xUnitinvi.TweetinviControllers.UserTests
{
    public class UserControllerTests
    {
        private readonly FakeClassBuilder<UserController> _fakeBuilder;
        private readonly Fake<IUserQueryExecutor> _fakeUserQueryExecutor;
        private readonly Fake<ITweetFactory> _fakeTweetFactory;
        private readonly Fake<IUserFactory> _fakeUserFactory;

        public UserControllerTests()
        {
            _fakeBuilder = new FakeClassBuilder<UserController>();
            _fakeUserQueryExecutor = _fakeBuilder.GetFake<IUserQueryExecutor>();
            _fakeTweetFactory = _fakeBuilder.GetFake<ITweetFactory>();
            _fakeUserFactory = _fakeBuilder.GetFake<IUserFactory>();
        }

        private UserController CreateUserController()
        {
            return _fakeBuilder.GenerateClass();
        }

        [Fact]
        public async Task GetFriendIds_ReturnsUserExecutorResult()
        {
            // Arrange
            var controller = CreateUserController();

            var parameters = new GetFriendIdsParameters("username");
            var expectedResult = A.Fake<ITwitterResult<IIdsCursorQueryResultDTO>>();

            _fakeUserQueryExecutor.CallsTo(x => x.GetFriendIds(A<IGetFriendIdsParameters>.Ignored, A<ITwitterRequest>.Ignored)).Returns(expectedResult);

            // Act
            var friendIdsIterator = controller.GetFriendIds(parameters, A.Fake<ITwitterRequest>());
            
            A.CallTo(() => _fakeUserQueryExecutor.FakedObject.GetFriendIds(A<IGetFriendIdsParameters>.Ignored, A<ITwitterRequest>.Ignored)).MustNotHaveHappened();

            var page = await friendIdsIterator.MoveToNextPage();

            A.CallTo(() => _fakeUserQueryExecutor.FakedObject.GetFriendIds(A<IGetFriendIdsParameters>.Ignored, A<ITwitterRequest>.Ignored)).MustHaveHappenedOnceExactly();

            // Assert
            Assert.Equal(page.TwitterResult, expectedResult);
        }

        [Fact]
        public async Task GetFollowerIds_ReturnsUserExecutorResult()
        {
            // Arrange
            var controller = CreateUserController();

            var parameters = new GetFollowerIdsParameters("username");
            var expectedResult = A.Fake<ITwitterResult<IIdsCursorQueryResultDTO>>();

            _fakeUserQueryExecutor.CallsTo(x => x.GetFollowerIds(A<IGetFollowerIdsParameters>.Ignored, A<ITwitterRequest>.Ignored)).Returns(expectedResult);

            // Act
            var friendIdsIterator = controller.GetFollowerIds(parameters, A.Fake<ITwitterRequest>());

            A.CallTo(() => _fakeUserQueryExecutor.FakedObject.GetFollowerIds(A<IGetFollowerIdsParameters>.Ignored, A<ITwitterRequest>.Ignored)).MustNotHaveHappened();

            var page = await friendIdsIterator.MoveToNextPage();

            A.CallTo(() => _fakeUserQueryExecutor.FakedObject.GetFollowerIds(A<IGetFollowerIdsParameters>.Ignored, A<ITwitterRequest>.Ignored)).MustHaveHappenedOnceExactly();

            // Assert
            Assert.Equal(page.TwitterResult, expectedResult);
        }

        [Fact]
        public async Task BlockUser_WithUser_ReturnsUserExecutorResult_False()
        {
            // Arrange
            var controller = CreateUserController();
            var userDTO = A.Fake<IUserDTO>();

            var blockUserParameters = new BlockUserParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            _fakeUserQueryExecutor.CallsTo(x => x.BlockUser(blockUserParameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.BlockUser(blockUserParameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async Task UnblockUser_ReturnsUserExecutorResult()
        {
            // Arrange
            var controller = CreateUserController();
            var userDTO = A.Fake<IUserDTO>();

            var parameters = new UnblockUserParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            _fakeUserQueryExecutor.CallsTo(x => x.UnblockUser(parameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.UnblockUser(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }
        
        [Fact]
        public async Task ReportUserFromSpam_ReturnsUserExecutorResult()
        {
            // Arrange
            var controller = CreateUserController();
            var userDTO = A.Fake<IUserDTO>();

            var parameters = new ReportUserForSpamParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            _fakeUserQueryExecutor.CallsTo(x => x.ReportUserForSpam(parameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.ReportUserForSpam(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}