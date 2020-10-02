﻿using System;
using Xunit;
using Moq;
using TodoApp.Repositories.XmlRepository.Utils;
using TodoApp.Repositories.Models;
using System.Xml.Linq;
using Xml = TodoApp.Repositories.XmlRepository;

namespace TodoApp.Tests.Repositories.TodoRepositories
{
    public class WhenContextHasData
    {
        protected Mock<IXmlContext> MockXmlContext;

        protected XElement Container;

        public WhenContextHasData()
        {
            // Arrange
            MockXmlContext = new Mock<IXmlContext>();

            var containerName = "todos";
            Container = new XElement(containerName);

            var firstTodo = new XElement("todo");
            firstTodo.Add(new XAttribute("Id", "00000000-0000-0000-0000-000000000000"));
            firstTodo.Add(new XElement("Title", "Unit tests"));
            firstTodo.Add(new XElement("Description", "Learn how to make unit tests"));
            firstTodo.Add(new XElement("Status", "Open"));
            firstTodo.Add(new XElement("CreatedOn", "2020-04-15T14:29:15.1823029Z"));
            firstTodo.Add(new XElement("DueDate", "2020-04-19T21:00:00.0000000Z"));

            var secondTodo = new XElement("todo");
            secondTodo.Add(new XAttribute("Id", "20975aeb-d490-4aa6-95ba-5b7c50b074a4"));
            secondTodo.Add(new XElement("Title", "Football"));
            secondTodo.Add(new XElement("Description", "Play football"));
            secondTodo.Add(new XElement("Status", "Open"));
            secondTodo.Add(new XElement("CreatedOn", "2020-04-16T10:17:33.2653554Z"));
            secondTodo.Add(new XElement("DueDate", "2020-05-29T21:00:00.0000000Z"));

            Container.Add(firstTodo);
            Container.Add(secondTodo);

            MockXmlContext
                .Setup(ctx => ctx.GetContainer(containerName))
                .Returns(Container);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("20975aeb-d490-4aa6-95ba-5b7c50b074a4")]
        public void GivenValidID_GetByID_ReturnsTodoEntity(string id)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);
            var guid = new Guid(id);

            var todo = repo.GetById(guid);

            Assert.NotNull(todo);
        }

        [Fact]
        public void GivenValidEntity_Update_UpdateEntity()
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);
            var guid = new Guid("20975aeb-d490-4aa6-95ba-5b7c50b074a4");
            var todo = repo.GetById(guid);

            todo.Title = "Concert";
            todo.Description = "Go to Metallica concert";
            repo.Update(todo);
            var newTodo = repo.GetById(guid);

            Assert.NotNull(newTodo);
            Assert.Equal(todo.Title, newTodo.Title);
            Assert.Equal(todo.Description, newTodo.Description);
        }

        [Theory]
        [InlineData("Picnic","Go to a picnic with friends",TodoStatus.Open,"2020-05-15T14:29:15.1823029Z","2020-05-19T21:00:00.0000000Z")]
        [InlineData("Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenValidTodo_Insert_ReturnsNewTodo(string title,string description, TodoStatus status, string createdOn, string dueDate)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var entity = new TodoModel()
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var todo = repo.Insert(entity);
            
            Assert.NotNull(todo);
        }

        [Theory]
        [InlineData("Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        [InlineData("Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenValidTodo_Insert_AddsTodo(string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var entity = new TodoModel()
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var todo = repo.Insert(entity);
            
            var guid = todo.Id;
            var addedTodo = repo.GetById(guid);

            Assert.NotNull(addedTodo);
        }

        [Fact]
        public void GivenValidElement_Delete_RemoveElement()
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var guid = new Guid("00000000-0000-0000-0000-000000000000");
            repo.Delete(guid);

            var todo = repo.GetById(guid);

            Assert.Null(todo);
        }
    }
}