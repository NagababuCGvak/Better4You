using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Better4You.UserManagement.Config
{
    [DataContract]
    public enum Menus
    {
        [EnumMember]
        [Description("None")]
        None = 0,

        [EnumMember]
        Menu1 = 1,

        [EnumMember]
        Menu2 = 2,

        [EnumMember]
        Menu3 = 3
    }
}
