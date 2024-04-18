using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Entities
{
    public class TaskTests
    {
        [Fact]
        public void Task_Initialization_Defaults()
        {
            // Arrange & Act
            var task = new Domain.Entities.Task();

            // Assert
            // Check that properties are initialized to default values
            Assert.Equal(0, task.Id); // Default value for an integer
            Assert.Null(task.Description); // Default value for a string
            Assert.Equal(DateTime.Now.Date, task.CreationDate.Date); // Default value for DateTime is the current date
            Assert.False(task.Solved); // Default value for a boolean (false)
            Assert.Equal(0, task.CustomerId); // Default value for an integer
            Assert.Null(task.Customer); // Default value for a navigation property (null)
        }

        [Fact]
        public void Task_PropertyGettersAndSetters()
        {
            // Arrange
            var task = new Domain.Entities.Task();
            var description = "Complete the project";
            var creationDate = DateTime.Now.AddDays(-1);
            var solved = true;
            var customerId = 1;

            // Act
            task.Description = description;
            task.CreationDate = creationDate;
            task.Solved = solved;
            task.CustomerId = customerId;

            // Assert
            Assert.Equal(description, task.Description);
            Assert.Equal(creationDate, task.CreationDate);
            Assert.Equal(solved, task.Solved);
            Assert.Equal(customerId, task.CustomerId);
        }

        [Fact]
        public void Task_Validation()
        {
            // Arrange
            var task = new Domain.Entities.Task();

            // Test setting null values for Required fields
            Assert.Throws<ValidationException>(() => task.Description = null);
        }

        [Fact]
        public void Task_RelationshipWithCustomer()
        {
            // Arrange
            var task = new Domain.Entities.Task();
            var customer = new Customer();

            // Act
            task.Customer = customer;
            task.CustomerId = customer.Id;

            // Assert
            Assert.NotNull(task.Customer);
            Assert.Equal(customer.Id, task.CustomerId);
        }
    }
}
