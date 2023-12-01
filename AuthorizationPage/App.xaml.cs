using AuthorizationPage.Databases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AuthorizationPage
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AuthorizationDB_Entities db = new AuthorizationDB_Entities();
        public static MainWindow mainWindow;
    }
}
