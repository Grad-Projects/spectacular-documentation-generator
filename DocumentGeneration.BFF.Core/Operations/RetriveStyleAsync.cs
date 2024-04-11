using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Operations
{
    public delegate Task<string> getStyleFromDB(string styleName);

    public delegate Task postDocumentToDB(string htmlFile, string styleName, string userName, string documentName);
    //public delegate Task CheckOrAddUser(string userName);
    public delegate Task checkUserInDB(string userName);
    public delegate Task addUserToDb(string userName);
    public delegate Task<bool> checkStyle(string styleName);
}
