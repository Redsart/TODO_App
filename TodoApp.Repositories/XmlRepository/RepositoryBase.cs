﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TodoApp.Repositories.Interfaces;
using TodoApp.Repositories.Models;
using System.Linq;
using System.IO;
using System.Data;

namespace TodoApp.Repositories.XmlRepository
{
    public abstract class RepositoryBase<TModel, TId> : IRepository<TModel, TId> where TModel : IModel<TId>
    {
        protected string Path { get; set; }
        protected XDocument Document { get; }
        protected XElement ContainerElement { get; }
        protected abstract string IdName { get; }

        protected XElement GetElementById(TId id)
        {
            XElement element = ContainerElement.Elements()
                .FirstOrDefault(a => a.Attribute(IdName).Value == id.ToString());

            return element;
        }

        protected RepositoryBase(string path, XName containerName)
        {
            Path = path;

            if (File.Exists(path))
            {
                Document = XDocument.Load(path);
            }
            else
            {
                Document = new XDocument(new XElement(containerName));
            }
            
            ContainerElement = Document.Element(containerName);
        }

        protected abstract TModel ElementToEntity(XElement element);
        protected abstract XElement EntityToElement(TModel entity);

        public IEnumerable<TModel> GetAll()
        {
            var models = from el
                         in ContainerElement.Elements()
                         select ElementToEntity(el);
            
            return models;
        }

        public void Delete(TId id)
        {
            GetElementById(id)
                .Remove();
        }

        public IEnumerable<TModel> Get(Func<TModel, bool> filter)
        {
            var models = GetAll().Where(filter);
            return models;
        }

        public IEnumerable<TModel> Get<TOrderKey>(Func<TModel, TOrderKey> orderByKey)
        {
            var models = GetAll().OrderBy(orderByKey);

            return models;
        }

        public IEnumerable<TModel> Get<TOrderKey>(Func<TModel, bool> filter, Func<TModel, TOrderKey> orderByKey)
        {
            var models = GetAll().Where(filter).OrderBy(orderByKey);

            return models;
        }

        public TModel GetById(TId id)
        {
            XElement element = GetElementById(id);

            TModel model = ElementToEntity(element);

            return model;
        }

        public TModel Insert(TModel model)
        {
            XElement newElement = EntityToElement(model);

            Guid id = Guid.NewGuid();
            newElement.Attribute(IdName).SetValue(id);
            ContainerElement.Add(newElement);

            TModel newModel = ElementToEntity(newElement);

            return newModel;
        }

        public bool Save()
        {
            Document.Save(Path);
            return true;
        }

        public void Update(TModel model)
        {
            var oldElement = GetElementById(model.Id);
            var newElement = EntityToElement(model);

            oldElement?.ReplaceWith(newElement);
        }
    }
}