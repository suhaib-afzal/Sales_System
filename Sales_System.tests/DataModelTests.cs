using Xunit;
using Class_Library;

namespace Sales_System.tests
{
    public class DataModelTests
    {
        [Fact]
        public void CartModel_EmptyPurchasesShouldTotal()
        {
            //Arrange
            decimal expected = 0;
            CartModel emptyCart = new CartModel();

            //Act
            decimal actual = emptyCart.CalculateTotal();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
