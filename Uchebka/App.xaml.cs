using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Uchebka.Components;

namespace Uchebka
{
    public partial class App : Application
    {
        public static UchebkaEntities2 db = new UchebkaEntities2();
        public static User user;
    }
}
