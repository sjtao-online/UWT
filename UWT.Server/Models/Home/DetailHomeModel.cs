using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Templates.Attributes.Details;

namespace UWT.Server.Models.Home
{
    [DetailModel]
    public class DetailHomeModel
    {
        [DetailItem("名称", DetailItemCategory.Default)]
        public string Name { get; set; }
        [DetailItem("标签")]
        public List<string> Tags { get; set; }
    }
}
