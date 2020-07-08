using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Consts;
using UWT.Templates.Attributes.Forms;

namespace UWT.Libs.Users.Roles
{
#pragma warning disable CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա�� XML ע��
    [FormModel("/Roles/ModifyModel")]
    public class RoleModifyModel : RoleAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
#pragma warning restore CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա�� XML ע��
}