using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels
{
    #region Normals
    public partial class UwtNormalsBanner : UWT.Libs.Normals.Banners.IDbBannerTable { }
    public partial class UwtNormalsFile : UWT.Libs.Normals.Files.IDbFileTable { }
    public partial class UwtNormalsNewsCate : UWT.Libs.Normals.News.IDbNewsCateTable { }
    public partial class UwtNormalsNews : UWT.Libs.Normals.News.IDbNewsTable { }

    #endregion
    #region Users
    public partial class UwtUsersAccount : UWT.Libs.Users.Users.IDbAccountTable { }
    public partial class UwtUsersMenuGroup : UWT.Libs.Users.MenuGroups.IDbMenuGroupTable { }
    public partial class UwtUsersRole : UWT.Libs.Users.Roles.IDbRoleTable { }
    public partial class UwtUsersLoginHis : UWT.Libs.Users.Users.IDbUserLoginHisTable { }
    public partial class UwtUsersMenuGroupItem : UWT.Libs.Users.MenuGroups.IDbMenuGroupItemTable { }
    public partial class UwtUsersRoleModuleRef : UWT.Libs.Users.Roles.IDbRoleModuleRefTable { }
    public partial class UwtUsersModule : UWT.Libs.Users.Roles.IDbModuleTable { }

    #endregion
    #region Helper
    public partial class UwtHelper : UWT.Libs.Helpers.Models.IDbHelperTable { }

    #endregion

    #region WeChats
    public partial class UwtWechatsUser : UWT.Libs.WeChats.Models.IDbWxUserModel { }
    #endregion
}
