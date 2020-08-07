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
    #region BBS
    public partial class UwtBbsArea : UWT.Libs.BBS.Models.IDbAreaTable { }
    public partial class UwtBbsAreaMgrRef : UWT.Libs.BBS.Models.IDbAreaMgrRefTable { }
    public partial class UwtBbsAreaTopicRef : UWT.Libs.BBS.Models.IDbAreaTopicRefTable { }
    public partial class UwtBbsTopic : UWT.Libs.BBS.Models.IDbTopicTable { }
    public partial class UwtBbsTopicBack : UWT.Libs.BBS.Models.IDbTopicBackTable { }
    public partial class UwtBbsTopicBackHis : UWT.Libs.BBS.Models.IDbTopicBackHisTable { }
    public partial class UwtBbsTopicHis : UWT.Libs.BBS.Models.IDbTopicHisTable { }
    public partial class UwtBbsUser : UWT.Libs.BBS.Models.IDbUserTable { }
    public partial class UwtBbsUserLevel : UWT.Libs.BBS.Models.IDbUserLevelTable { }
    public partial class UwtBbsUserLevelType : UWT.Libs.BBS.Models.IDbUserLevelTypeTable { }
    #endregion
}
