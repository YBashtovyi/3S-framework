using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Enums
{
    public enum FileStoreDestinationType
    {
        // store file on the application server
        Local,
        // store file somewhere on remote (media) server
        Remote,
        // file is stored in database
        DataBase
    }
}
