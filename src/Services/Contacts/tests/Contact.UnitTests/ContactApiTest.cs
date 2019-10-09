using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using Contact.API.Controllers;
using Contact.API.DTOs.Contact;
using Contact.API.IntegrationEvents.Events;
using Contact.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Contact.UnitTests
{
    public class ContactApiTest
    {
        private readonly Mock<IEventBus> _serviceBusMock;
        private readonly Mock<ContactContext> _contactContext;

        public ContactApiTest()
        {
            _serviceBusMock = new Mock<IEventBus>(); //TO DO
            _contactContext = CreateDbContext();
        }

        #region ContactController

        //[Fact]
        //public void GetContactsForDataTable_Ok()
        //{
        //    //Arrange
        //    var fakeTotalNumberOfContacts = 1;
        //    var fakeFirstName = "Kristina";
        //    var fakeListStrings = new List<string>() { "TestList" };
        //    // int[] fakeListIds = new int[] { 1 };
        //    string fakeListIds = "1";

        //    //Act
        //    var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
        //    var actionResult = contactController.GetContactsForDataTable(fakeListIds, true);

        //    //Assert
        //    var okResult = Assert.IsType<OkObjectResult>(actionResult);
        //    Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);

        //    var returnValue = Assert.IsAssignableFrom<IQueryable<ContactsDataTableDTO>>(okResult.Value);
        //    Assert.Equal(fakeTotalNumberOfContacts, returnValue.Count());
        //    Assert.Equal(fakeFirstName, returnValue.FirstOrDefault().FirstName);
        //    // Assert.Equal(fakeListStrings, returnValue.FirstOrDefault().Lists);
        //}

        //[Fact]
        //public void GetContactsForDataTable_BadRequest()
        //{
        //    //Arrange
        //    //int[] fakeListIds = new int[] { 50 };
        //    string fakeListIds = "1";

        //    //Act
        //    var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
        //    var actionResult = contactController.GetContactsForDataTable(fakeListIds, true);

        //    //Assert
        //    var result = Assert.IsType<BadRequestResult>(actionResult);
        //    Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        //}

        [Fact]
        public async Task GetContacts_Ok()
        {
            //Arrange
            var fakeTotalNumberOfContacts = 2;
            var fakeFirstName = "Kristina";

            //Act
            var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await contactController.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);

            var returnValue = Assert.IsType<List<GetContactDTO>>(okResult.Value);
            Assert.Equal(fakeTotalNumberOfContacts, returnValue.Count);
            Assert.Equal(fakeFirstName, returnValue.FirstOrDefault().FirstName);
        }

        [Fact]
        public async Task GetContactsInList_Ok()
        {
            //Arrange
            var fakeListId = 1;
            var fakeTotalNumberOfContacts = 1;
            var fakeFirstName = "Kristina";

            //Act
            var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await contactController.GetContactsInList(fakeListId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);

            var returnValue = Assert.IsType<List<GetContactDTO>>(okResult.Value);
            Assert.Equal(fakeTotalNumberOfContacts, returnValue.Count);
            Assert.Equal(fakeFirstName, returnValue.FirstOrDefault().FirstName);
        }

        [Fact]
        public async Task GetContactsInList_NoContent()
        {
            //Arrange
            var listIdWithNoContacts = 2;

            //Act
            var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await contactController.GetContactsInList(listIdWithNoContacts);

            //Assert
            var result = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GetContactsInList_BadRequest()
        {
            //Arrange
            var nonExistentListId = 50;

            //Act
            var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await contactController.GetContactsInList(nonExistentListId);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Create_BadRequest_InvalidModel()
        {
            //Arrange
            var emailThatAlreadyExists = "kris@gmail.com";

            var model = new CreateContactDTO()
            {
                FirstName = "test",
                LastName = "test",
                Email = emailThatAlreadyExists,
                Active = true
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Create(model);
            
            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Create_BadRequest_ListDoesntExist()
        {
            //Arrange
            int nonExistingList = 10;

            var contactDTO = new CreateContactDTO()
            {
                FirstName = "test",
                LastName = "test",
                Email = "test@test.test",
                Active = true,
                ListIds = new List<int>() { nonExistingList }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Create(contactDTO);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Create_Created()
        {
            //Arrange
            var firstName = "Elizabeth";
            var lastName = "Johnson";
            var email = "newcontact@gmail.com";
            var phone = "0635555";
            var active = true;

            var contactDTO = new CreateContactDTO()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Active = active
            };

            //Act
            var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await contactController.Create(contactDTO);

            //Assert
            var result = Assert.IsType<CreatedAtActionResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.Created, result.StatusCode);
            var returnValue = Assert.IsType<int>(result.Value);
        }

        [Fact]
        public async Task Update_BadRequest_InvalidModel()
        {
            //Arrange
            var emailThatAlreadyExists = "john@gmail.com";
            var model = new UpdateContactDTO()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = emailThatAlreadyExists,
                Active = true
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Update(model);

            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Update_BadRequest_ContactDoesntExist()
        {
            //Arrange
            var contactIdThatDoesntExist = 10;
            var model = new UpdateContactDTO()
            {
                Id = contactIdThatDoesntExist,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.test",
                Active = true
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Update(model);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task Update_BadRequest_ListDoesntExist()
        {
            //Arrange
            var listThatDoesntExist = 10;
            var model = new UpdateContactDTO()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.test",
                Active = true,
                ListIds = new List<int>() { listThatDoesntExist }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Update(model);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task Update_Ok()
        {
            //Arrange
            var id = 1;
            var firstName = "Elizabeth";
            var lastName = "Johnson";
            var email = "newcontact@gmail.com";
            var phone = "0635555";
            var active = true;

            var contactDTO = new UpdateContactDTO()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Active = active,
                ListIds = new List<int>()
            };

            //Act
            var contactController = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await contactController.Update(contactDTO);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Delete_BadRequest_ContactDoesntExist()
        {
            //Arrange
            var contactIdThatDoesntExist = 10;
            var contactIds = new List<int>() { contactIdThatDoesntExist };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Delete(contactIds);

            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            var returnValue = Assert.IsType<string>(result.Value);
        }

        [Fact]
        public async Task Delete_Ok()
        {
            //Arrange
            var validContactId = 1;
            var contactIds = new List<int>() { validContactId };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            controller.ControllerContext = FakeControllerContext();

            var actionResult = await controller.Delete(contactIds);
            _serviceBusMock.Verify(mock => mock.Publish(It.IsAny<ContactsDeletedIntegrationEvent>()), Times.Once);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Activate_BadRequest_ContactDoesntExist()
        {
            //Arrange
            var contactIdThatDoesntExist = 10;
            var contactIds = new List<int>() { contactIdThatDoesntExist };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Activate(contactIds);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Activate_Ok()
        {
            //Arrange
            var validContactId = 1;
            var contactIds = new List<int>() { validContactId };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Activate(contactIds);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Deactivate_BadRequest_ContactDoesntExist()
        {
            //Arrange
            var contactIdThatDoesntExist = 10;
            var contactIds = new List<int>() { contactIdThatDoesntExist };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Deactivate(contactIds);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Deactivate_Ok()
        {
            //Arrange
            var validContactId = 1;
            var contactIds = new List<int>() { validContactId };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.Deactivate(contactIds);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task AddContactsToList_BadRequest_ModelNotValid()
        {
            //Arange
            var model = new ContactListDTO();

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.AddContactsToLists(model);

            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsType<SerializableError>(result.Value);
        }

        [Fact]
        public async Task AddContactsToList_BadRequest_ContactDoesntExist()
        {
            //Arange
            int contactIdThatDoesntExist = 10;
            int validListId = 1;

            var model = new ContactListDTO()
            {
                ContactIds = new List<int>() { contactIdThatDoesntExist },
                ListIds = new List<int>() { validListId }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.AddContactsToLists(model);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task AddContactsToList_BadRequest_ListDoesntExist()
        {
            //Arange
            int validContactId = 1;
            int listIdThatDoesntExist = 10;

            var model = new ContactListDTO()
            {
                ContactIds = new List<int>() { validContactId },
                ListIds = new List<int>() { listIdThatDoesntExist }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.AddContactsToLists(model);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task AddContactsToList_Ok()
        {
            //Arange
            int validContactId = 1;
            int validListId = 2;

            var model = new ContactListDTO()
            {
                ContactIds = new List<int>() { validContactId },
                ListIds = new List<int>() { validListId }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.AddContactsToLists(model);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task RemoveContactsFromLists_BadRequest_ModelNotValid()
        {
            //Arange
            var model = new ContactListDTO();

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.RemoveContactsFromLists(model);

            //Assert
            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsType<SerializableError>(result.Value);
        }

        [Fact]
        public async Task RemoveContactsFromLists_BadRequest_ContactDoesntExist()
        {
            //Arange
            int contactIdThatDoesntExist = 10;
            int validListId = 1;

            var model = new ContactListDTO()
            {
                ContactIds = new List<int>() { contactIdThatDoesntExist },
                ListIds = new List<int>() { validListId }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.RemoveContactsFromLists(model);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task RemoveContactsFromLists_ListDoesntExist()
        {
            //Arange
            int validContactId = 1;
            int listIdThatDoesntExist = 10;

            var model = new ContactListDTO()
            {
                ContactIds = new List<int>() { validContactId },
                ListIds = new List<int>() { listIdThatDoesntExist }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.RemoveContactsFromLists(model);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task RemoveContactsFromLists_Ok()
        {
            //Arange
            int validContactId = 1;
            int validListId = 2;

            var model = new ContactListDTO()
            {
                ContactIds = new List<int>() { validContactId },
                ListIds = new List<int>() { validListId }
            };

            //Act
            var controller = new ContactController(_serviceBusMock.Object, _contactContext.Object);
            var actionResult = await controller.RemoveContactsFromLists(model);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        #endregion

        #region ListController
        #endregion

        private ControllerContext FakeControllerContext()
        {
            var fakeIdentity = new GenericIdentity("Kris");
            var fakeUser = new GenericPrincipal(fakeIdentity, null);
            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = fakeUser } };

            return controllerContext;
        }

        private Mock<ContactContext> CreateDbContext()
        {
            var contacts = GetFakeContacts().AsQueryable();

            var contactDbSet = new Mock<DbSet<Contact.API.Models.Contact>>();

            contactDbSet.As<IAsyncEnumerable<Contact.API.Models.Contact>>().Setup(m => m.GetEnumerator()).Returns(new TestAsyncEnumerator<Contact.API.Models.Contact>(contacts.GetEnumerator()));
            contactDbSet.As<IQueryable<Contact.API.Models.Contact>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Contact.API.Models.Contact>(contacts.Provider));
            contactDbSet.As<IQueryable<Contact.API.Models.Contact>>().Setup(m => m.Expression).Returns(contacts.Expression);
            contactDbSet.As<IQueryable<Contact.API.Models.Contact>>().Setup(m => m.ElementType).Returns(contacts.ElementType);
            contactDbSet.As<IQueryable<Contact.API.Models.Contact>>().Setup(m => m.GetEnumerator()).Returns(() => contacts.GetEnumerator());

            var lists = GetFakeLists().AsQueryable();

            var listDbSet = new Mock<DbSet<List>>();
            listDbSet.As<IAsyncEnumerable<List>>().Setup(m => m.GetEnumerator()).Returns(new TestAsyncEnumerator<List>(lists.GetEnumerator()));
            listDbSet.As<IQueryable<List>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<List>(lists.Provider));
            listDbSet.As<IQueryable<List>>().Setup(m => m.Expression).Returns(lists.Expression);
            listDbSet.As<IQueryable<List>>().Setup(m => m.ElementType).Returns(lists.ElementType);
            listDbSet.As<IQueryable<List>>().Setup(m => m.GetEnumerator()).Returns(() => lists.GetEnumerator());

            var contactLists = GetFakeContactLists().AsQueryable();

            var contactListDbSet = new Mock<DbSet<ContactList>>();
            contactListDbSet.As<IAsyncEnumerable<ContactList>>().Setup(m => m.GetEnumerator()).Returns(new TestAsyncEnumerator<ContactList>(contactLists.GetEnumerator()));
            contactListDbSet.As<IQueryable<ContactList>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<ContactList>(contactLists.Provider));
            contactListDbSet.As<IQueryable<ContactList>>().Setup(m => m.Expression).Returns(contactLists.Expression);
            contactListDbSet.As<IQueryable<ContactList>>().Setup(m => m.ElementType).Returns(contactLists.ElementType);
            contactListDbSet.As<IQueryable<ContactList>>().Setup(m => m.GetEnumerator()).Returns(() => contactLists.GetEnumerator());

            var mockContext = new Mock<ContactContext>();
            mockContext.Setup(c => c.Contacts).Returns(contactDbSet.Object);
            mockContext.Setup(l => l.Lists).Returns(listDbSet.Object);
            mockContext.Setup(cl => cl.ContactLists).Returns(contactListDbSet.Object);

            return mockContext;
        }

        private List<Contact.API.Models.Contact> GetFakeContacts()
        {
            var fListNavigation = new List()
            {
                Id = 1,
                ListName = "TestList",
                Description = "Desc",
                ParentId = null
            };

            return new List<Contact.API.Models.Contact>()
            {
                new Contact.API.Models.Contact
                {
                    Id=1,
                    FirstName="Kristina",
                    LastName="Staletovic",
                    Email="kris@gmail.com",
                    Phone="0633333",
                    Active=true,
                    ContactLists = new List<ContactList>()
                    {
                        new ContactList
                        {
                            FContact = 1,
                            FList = 1,
                            FListNavigation = fListNavigation
                        }
                    }
                },
                new Contact.API.Models.Contact
                {
                    Id=2,
                    FirstName="John",
                    LastName="Smith",
                    Email="john@gmail.com",
                    Phone="0633333",
                    Active=true,
                    ContactLists = new List<ContactList>()
                }
            };
        }

        private List<List> GetFakeLists()
        {
            var fContactNavigation = new Contact.API.Models.Contact()
            {
                Id = 1,
                FirstName = "Kristina",
                LastName = "Staletovic",
                Email = "kris@gmail.com",
                Phone = "0633333",
                Active = true
            };

            return new List<List>()
            {
                new List
                {
                    Id = 1,
                    ListName = "TestList",
                    Description = "Desc",
                    ParentId = null,
                    ContactLists = new List<ContactList>()
                    {
                        new ContactList
                        {
                            FContact = 1,
                            FList = 1,
                            FContactNavigation = fContactNavigation
                        }
                    },
                    ChildLists = new List<List>()
                },
                new List
                {
                    Id=2,
                    ListName = "SecondList",
                    Description = "Desc",
                    ParentId = null,
                    ContactLists = new List<ContactList>(),
                    ChildLists = new List<List>()
                }
            };
        }

        private List<ContactList> GetFakeContactLists()
        {
            return new List<ContactList>()
            {
                new ContactList
                {
                    FContact = 1,
                    FList = 1
                }
            };
        }
    }

    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }
    }
}
