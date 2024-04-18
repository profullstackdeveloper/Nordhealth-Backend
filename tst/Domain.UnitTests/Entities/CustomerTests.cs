using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Initialization_Defaults()
        {
            // Arrange & Act
            var customer = new Customer();

            // Assert
            // Check that properties are initialized to default values
            Assert.Equal(0, customer.Id); // Default value for an integer
            Assert.Null(customer.FirstName); // Default value for a string
            Assert.Null(customer.LastName); // Default value for a string
            Assert.Equal(default(DateTime), customer.Birthday); // Default value for DateTime
            Assert.Empty(customer.Contacts); // Default value for a collection
            Assert.Empty(customer.Tasks); // Default value for a collection
        }

        [Fact]
        public void Customer_PropertyGettersAndSetters()
        {
            // Arrange
            var customer = new Customer();
            var firstName = "John";
            var lastName = "Doe";
            var birthday = new DateTime(1990, 1, 1);

            // Act
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Birthday = birthday;

            // Assert
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(birthday, customer.Birthday);
        }

        [Fact]
        public void Customer_ContactsInitialization()
        {
            // Arrange
            var customer = new Customer();

            // Act
            // No action required; we want to test the initialization

            // Assert
            // Check that the Contacts collection is initialized as an empty collection
            Assert.Empty(customer.Contacts);
        }

        [Fact]
        public void Customer_TasksInitialization()
        {
            // Arrange
            var customer = new Customer();

            // Act
            // No action required; we want to test the initialization

            // Assert
            // Check that the Tasks collection is initialized as an empty collection
            Assert.Empty(customer.Tasks);
        }
    }
}
