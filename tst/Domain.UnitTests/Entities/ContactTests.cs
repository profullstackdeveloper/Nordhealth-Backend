using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.UnitTests.Entities
{
    public class ContactTests
    {
        [Fact]
        public void Contact_Initialization_Defaults()
        {
            // Arrange & Act
            var contact = new Contact();

            // Assert
            // Check that properties are initialized to default values
            Assert.Equal(0, contact.Id); // Default value for an integer
            Assert.Equal(default(ContactType), contact.Type); // Default value for an enum
            Assert.Null(contact.Value); // Default value for a string
            Assert.Equal(0, contact.CustomerId); // Default value for an integer
            Assert.Null(contact.Customer); // Default value for a navigation property (null)
        }

        [Fact]
        public void Contact_PropertyGettersAndSetters()
        {
            // Arrange
            var contact = new Contact();
            var type = ContactType.Phone;
            var value = "555-1234";
            var customerId = 1;

            // Act
            contact.Type = type;
            contact.Value = value;
            contact.CustomerId = customerId;

            // Assert
            Assert.Equal(type, contact.Type);
            Assert.Equal(value, contact.Value);
            Assert.Equal(customerId, contact.CustomerId);
        }

        [Fact]
        public void Contact_Validation()
        {
            // Arrange
            var contact = new Contact();

            // Act
            // Set values that violate data annotations

            // Test setting value exceeding MaxLength
            Assert.Throws<ValidationException>(() => contact.Value = new string('a', 101));

            // Test setting null values for Required fields
            Assert.Throws<ValidationException>(() => contact.Type = default);
            Assert.Throws<ValidationException>(() => contact.Value = null);
        }

        [Fact]
        public void Contact_RelationshipWithCustomer()
        {
            // Arrange
            var contact = new Contact();
            var customer = new Customer();

            // Act
            contact.Customer = customer;
            contact.CustomerId = customer.Id;

            // Assert
            Assert.NotNull(contact.Customer);
            Assert.Equal(customer.Id, contact.CustomerId);
        }
    }

}
