using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uchebka.Components
{
    public partial class User
    {
        public int Age 
        { 
            get
            {
                if (DateBithday == null) return 0;
                else return DateTime.Today.Year - DateBithday.Value.Year;
            }
        }

        public string Operations
        {
            get
            {
                if (App.db.UserOperation.Where(x => x.Login == Login).Count() == 0) return "-";
                else
                {
                    List<string> op = new List<string>();
                    foreach (UserOperation usop in App.db.UserOperation.Where(x => x.Login == Login))
                    {
                        op.Add( App.db.Operation.First(x => x.Id == usop.IdOperation).Name);
                    }
                    return string.Join(", ", op.ToArray());
                }
            }
        }
    }
}
