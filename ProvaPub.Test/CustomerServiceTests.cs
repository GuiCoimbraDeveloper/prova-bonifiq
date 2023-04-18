using Moq;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Test
{
    public class CustomerServiceTests
    {
        private readonly TestDbContext _ctx;
        private readonly CustomerService _cs;
        public CustomerServiceTests()
        {
            _ctx = TestDatabaseFixture.CreateContext();
            _cs = new CustomerService(_ctx);
        }

        //[Fact(DisplayName = "")]
        [Theory]
        [InlineData(3, 10)]
        [InlineData(2, 20)]
        public async Task GivenPurchase_WhenIsValid_ThenShouldSucccessPurchase(int customerId, decimal purchaseValue)
        {
            //arrange
            var resultExpected = true;

            //act
            var resultActual = await _cs.CanPurchase(customerId, purchaseValue);

            //assert
            Assert.Equal(resultExpected, resultActual);
        }

        [Fact]
        [Trait("Customer", "Id")]
        public async Task GivenError_WhenCustomerIdIsZero_ThenShouldException()
        {
            //act
            Func<Task> act = async () => await _cs.CanPurchase(0, 10);
            //assert
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
            Assert.Equal("customerId", exception.ParamName);
        }
        [Fact]
        [Trait("Purchase", "Value")]
        public async Task GivenError_WhenPurchaseValueIsZero_ThenShouldExcpetion()
        {
            //act
            Func<Task> act = async () => await _cs.CanPurchase(1, 0);
            //assert
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
            Assert.Equal("purchaseValue", exception.ParamName);
        }
        [Fact]
        [Trait("Purchase", "Value")]
        public async Task GivenError_WhenCustomerIdIsInvalid_ThenShouldExcpetion()
        {
            //act
            Func<Task> act = async () => await _cs.CanPurchase(200, 20);
            //assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }
        [Fact]
        [Trait("Purchase", "Value")]
        public async Task GivenError_WhenCustomerPurchaseOneTimePerMonth_ThenShouldExcpetion()
        {
            //arrange
            var resultExpected = false;
            //act
            var resultActual = await _cs.CanPurchase(1, 20);
            //assert
            Assert.Equal(resultExpected, resultActual);
        }

        [Fact]
        [Trait("Purchase", "Value")]
        public async Task GivenError_WhenCustomerFirstPurchaseMaxIsOneHundred_ThenShouldExcpetion()
        {
            //arrange
            var resultExpected = false;
            //act
            var resultActual = await _cs.CanPurchase(2, 120);
            //assert
            Assert.Equal(resultExpected, resultActual);
        }
    }
}
