﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Repositorys
{
    public interface IRepository<T>
    {
        void Insert(T item);
        void Update(T item);
        T GetById(long id);
        List<T> GetAll();
    }
}