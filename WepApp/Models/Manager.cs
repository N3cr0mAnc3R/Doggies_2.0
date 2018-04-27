using DarkSide;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WepApp.Models
{
    public abstract class Manager : IDisposable
    {
        protected Concrete Concrete { get; private set; }
                
        public void Dispose()
        {
            Concrete.Dispose();
        }
    }
}