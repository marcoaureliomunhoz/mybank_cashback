using AutoFixture;
using Moq;
using MyBank.Cashback.Domain.Dtos;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;
using MyBank.Cashback.Domain.Services;
using Xunit;

namespace MyBank.Cashback.Test.Domain
{
    public class CashbackServiceTest
    {
        private readonly Fixture _fixture;
        private readonly CashbackService _cashbackService;
        private readonly Mock<ICashbackRepository> _cashbackRepositoryMock;
        private readonly CashbackConfiguration _cashbackConfiguration;

        public CashbackServiceTest()
        {
            _fixture = new Fixture();

            _cashbackRepositoryMock = new Mock<ICashbackRepository>();
            _cashbackConfiguration = new CashbackConfiguration();

            _cashbackService = new CashbackService(_cashbackRepositoryMock.Object);
        }

        [Fact]
        public void ShouldNotInsertAccount()
        {
            var dto = _fixture.Build<NewTransactionCashbackDto>().Create();
            _cashbackRepositoryMock
                .Setup(m => m.ExistsAccountByTransaction(dto.TransactionId))
                .Returns(true);

            _cashbackService.InsertCashback(dto);

            _cashbackRepositoryMock
                .Verify(m => m.InsertAccount(It.IsAny<CashbackAccount>()), Times.Never);
        }
    }
}