using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uchebka.Components
{
    public partial class Order
    {
        public string ManagerName
        {
            get
            {
                if (LoginManager == null) return "";
                else return App.db.User.First(x => x.Login == LoginManager).LastName;
            }
        }
    }
}
