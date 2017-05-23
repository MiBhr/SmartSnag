using SmartSnag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSnag.Domain.Abstract
{
    public interface IDefectItemReposity
    {
        IEnumerable<DefectItem> Defects { get; }
    }
}
