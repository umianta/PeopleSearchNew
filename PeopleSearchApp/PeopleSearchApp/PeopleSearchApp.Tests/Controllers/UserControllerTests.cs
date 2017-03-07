using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchApp.Controllers;
using PeopleSearchApp.Models;
using PeopleSearchApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Web.Mvc;
using Moq;
namespace PeopleSearchApp.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        
        [TestMethod()]
        public void UserControllerTest()
        {
           
        }

        [TestMethod()]
        public void PeopleListTest()
        {
            Mock<IRepository<User>> mock = new Mock<IRepository<User>>();
            mock.Setup(m => m.GetAll()).Returns(new User[]
            {
                new User {Id=100,FirstName="David1",LastName="Power1",Age=23, Interests="Reading" },
                new User {Id=101,FirstName="David2",LastName="Power2",Age=23, Interests="Reading" },
            }.AsEnumerable());

            UserController controller = new UserController(mock.Object);

            //Act
            var actual = (IEnumerable<Models.User>) controller.PeopleList().Model;

            //Assert 
            NUnit.Framework.Assert.IsInstanceOf<IEnumerable<Models.User>>(actual);
        }
              
    }
}